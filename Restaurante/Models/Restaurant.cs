using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.Models
{
    public class Restaurant
    {
        public int id { get; set; }
        [Display(Name="Nombre del Restaurante")]
        public string Name { get; set; }
        public CuisineType Cuisine { get; set; } 

    }
}
