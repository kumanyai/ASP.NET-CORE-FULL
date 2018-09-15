//Esta clase no necesita derivar de otras clases especificas
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

namespace Restaurante.Controllers
{
    public class HomeController
    {
        public string Index() //Verificamos si existe un metodo Index
        {
            return "Hola desde el HomeController!";
        }
    }
}
