using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurante.Data;
using Restaurante.Models;

namespace Restaurante.Services
{
    public class SqlRestaurantData : IRestaurantData
    {
        private RestauranteDbContext _context;

        public SqlRestaurantData(RestauranteDbContext context)
        {
            _context = context;
        }
        public Restaurant Add(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
            return restaurant;
        }

        public Restaurant Get(int id)
        {
            return _context.Restaurants.FirstOrDefault(r => r.id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _context.Restaurants.OrderBy(r => r.Name);
        }
    }
}
