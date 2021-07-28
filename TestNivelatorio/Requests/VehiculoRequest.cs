using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestNivelatorio.Requests
{
    public class VehiculoRequest
    {
        public string Patente { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Puertas { get; set; }
        public string Titular { get; set; }
    }
}
