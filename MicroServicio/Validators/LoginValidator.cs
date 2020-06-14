using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServicio.Validators
{
    public class LoginValidator
    {
        [Required(ErrorMessage = "La cedula es requerida.")]
        public string cedula { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida.")]
        public string password { get; set; }
    }
}
