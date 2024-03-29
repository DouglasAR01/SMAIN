﻿using System;
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
        public string nombre_1 { get; set; }
        public string nombre_2 { get; set; }
        public string apellido_1 { get; set; }
        public string apellido_2 { get; set; }
        public string role  { get; set; }
        [Required(ErrorMessage = "La contraseña no puede ser nula")]
        public string password { get; set; }
        public string token { get; set; }

        //Relación 1 a n cuentas.
        public ICollection<Cuenta> cuentas { get; set; }

    }
}
