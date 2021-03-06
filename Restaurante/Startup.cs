﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Restaurante.Data;
using Restaurante.Services;
using Resturante.Services;

namespace Restaurante
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme; //Forzamos al usuario a Autenticarse

            })
            .AddOpenIdConnect(options => 
            {
                _configuration.Bind("AzureAd", options);                                
            })
            .AddCookie();

            //PONDREMOS NUESTROS INTERFACES/SERVICIOS PERSONALIZADOS PARA INYECTAR EN OTROS COMPONENTES
            //InvalidOperationException: No service for type 'Restaurante.IGreeter' has been registered.
            services.AddSingleton<IGreeter, Greeter>();//CREAMOS LA INSTANCIA DE LA CLASE IGreeter cuando necesiten el IGreeter
            /*services.AddScoped<IRestaurantData, InMemoryRestaurantData>();*///Creamos una instanacia con el ADDSCOPED para solicitudes HTTP
            services.AddMvc();
            services.AddDbContext<RestauranteDbContext>(options => 
            options.UseSqlServer(_configuration.GetConnectionString("Restaurante")));
            services.AddScoped<IRestaurantData, SqlRestaurantData>();//Aseguramos que la conexion sea de un solo hilo.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IHostingEnvironment env,
                              IGreeter Greeter,
                              ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();//Excepcion de pagina
            }

            //app.UseFileServer();//Servimos archivos

            app.UseStaticFiles();//Servimos archivos estaticos

            app.UseAuthentication();

            /*app.UseMvcWithDefaultRoute();*///Agregamos MVC y busca un archivo controlador
            app.UseMvc(ConfigureRoutes);

            app.UseRewriter(new RewriteOptions().AddRedirectToHttpsPermanent());

            app.Run(async (context) =>
            {
                var greeting = Greeter.GetMessageOfTheDay();
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync($"Not Found");
            });
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            // /home/index
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
