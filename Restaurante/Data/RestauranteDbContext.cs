using Microsoft.EntityFrameworkCore;
using Restaurante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante.Data
{
    public class RestauranteDbContext : DbContext
    {
        public RestauranteDbContext(DbContextOptions options)
            : base(options)
        {

        }
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
