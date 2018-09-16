using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante.Controllers
{
    //about
    [Route("company/[controller]/[action]")]
    public class AboutController
    {
        public string Phone()
        {
            return "505+84196736";
        }
        public string Address()
        {
            return "NIC";
        }
    }
}
