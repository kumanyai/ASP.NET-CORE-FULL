
using Microsoft.AspNetCore.Mvc;
using Restaurante.Models;
using Restaurante.Services;

namespace Restaurante.Controllers
{
    public class HomeController : Controller
    {
        private IRestaurantData _restaurantData;

        public HomeController(IRestaurantData restaurantData)
        {
            _restaurantData = restaurantData;
        }

        public IActionResult Index() //Verificamos si existe un metodo Index
        {
            var model = _restaurantData.GetAll();
            /*return new ObjectResult(model);*///enviamos los datos serializados en JSON
            return View(model);

        }
    }
}
