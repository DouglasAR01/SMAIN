using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServicio.Entities
{
    public class Usuario
    {
        [Key]
        public ulong id { get; set; }
        [Required(ErrorMessage = "La cédula no puede ser nula")]
        public string cedula { get; set; }
        //[Required(ErrorMessage = "Ingrese su primer nombre")]
        public string nombre_1 { get; set; }
        public string nombre_2 { get; set; }
        //[Required(ErrorMessage = "Ingrese su primer apellido")]
        public string apellido_1 { get; set; }
        public string apellido_2 { get; set; }
        //[Required(ErrorMessage = "El rol no debe ser nulo")]
        public string role  { get; set; }
        [Required(ErrorMessage = "La contraseña no puede ser nula")]
        public string password { get; set; }
        public string token { get; set; }


    }
}
