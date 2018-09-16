
using Microsoft.AspNetCore.Mvc;
using Restaurante.Models;
using Restaurante.Services;
using Restaurante.ViewModels;
using Resturante.Services;

namespace Restaurante.Controllers
{
    public class HomeController : Controller
    {
        private IRestaurantData _restaurantData;
        private IGreeter _greeter;

        public HomeController(IRestaurantData restaurantData,
                              IGreeter greeter)
        {
            _restaurantData = restaurantData;
            _greeter = greeter;
        }

        public IActionResult Index() //Verificamos si existe un metodo Index
        {
            var model = new HomeIndexViewModel();
            model.Restaurants = _restaurantData.GetAll();
            model.CurrentMessage = _greeter.GetMessageOfTheDay();
            /*return new ObjectResult(model);*///enviamos los datos serializados en JSON
            return View(model);

        }

        public IActionResult Details(int id)
        {
            //return Content(id.ToString());//Verificamos el ID que pasamos para verificar si es el correcto
            var model = _restaurantData.Get(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));//Retorna un error 404 HTTP cuando no encuentra la solicitud
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RestaurantEditModel model)
        {
            //return Content("POST");
            if (ModelState.IsValid)
            {
            
            var newRestaurant = new Restaurant();
            newRestaurant.Name = model.Name;
            newRestaurant.Cuisine = model.Cuisine;

            newRestaurant = _restaurantData.Add(newRestaurant);

            return RedirectToAction(nameof(Details), new {id=newRestaurant});
            }
            else
            {
                return View();
            }
        }

    }
}
