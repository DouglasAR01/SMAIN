using MicroServicio.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServicio.Validators
{
    public class UsuarioValidator
    {
        [Required(ErrorMessage = "La cédula no puede ser nula")]
        [RegularExpression(@"^\d+$", ErrorMessage = "La cédula es un número positivo,")]
        public string cedula { get; set; }
        [Required(ErrorMessage = "Ingrese su primer nombre")]
        [StringLength(30, ErrorMessage = "El primer nombre no puede superar los 30 caracteres.")]
        public string nombre_1 { get; set; }
        [StringLength(30, ErrorMessage = "El segundo nombre no puede superar los 30 caracteres.")]
        public string nombre_2 { get; set; }
        [Required(ErrorMessage = "Ingrese su primer apellido")]
        [StringLength(30, ErrorMessage = "El primer apellido no puede superar los 30 caracteres.")]
        public string apellido_1 { get; set; }
        [StringLength(30, ErrorMessage = "El segundo apellido no puede superar los 30 caracteres.")]
        public string apellido_2 { get; set; }
        [Required(ErrorMessage = "El rol no debe ser nulo")]
        [RegularExpression(@"^[a|u]{1}$", ErrorMessage = "El rol debe ser 'a' o 'u'")]
        public string role { get; set; }
        [Required(ErrorMessage = "La contraseña no puede ser nula")]
        public string password { get; set; }
        public string token { get; set; }

    }
}
