using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestNivelatorio.Models
{
    public class Vehiculo
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(8)]
        public string Patente { get; set; }
        [Required]
        public string Marca { get; set; }
        [Required]
        public string Modelo { get; set; }
        [Required]
        public int Puertas { get; set; }
        [Required]
        public string Titular { get; set; }
    }
}
