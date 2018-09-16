using Restaurante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante.Services
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        public List<Restaurant> _restaurants;

        public InMemoryRestaurantData()
        {
            _restaurants = new List<Restaurant>
            {
                new Restaurant {id = 1, Name = "Alex Pizza"},
                new Restaurant {id = 2, Name = "Charlie Pizza"},
                new Restaurant {id = 3, Name = "Bessy Pizza"},
                new Restaurant {id = 4, Name = "Julio Pizza"}
            };
               
        }

        public Restaurant Add(Restaurant restaurant)
        {
            restaurant.id = _restaurants.Max(r => r.id) + 1;
            _restaurants.Add(restaurant);
            return restaurant;
        }

        public Restaurant Get(int id)
        {
            return _restaurants.FirstOrDefault(r => r.id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _restaurants.OrderBy(r => r.Name);
        }
    }
}
