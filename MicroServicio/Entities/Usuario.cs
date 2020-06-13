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
        public string cedula { get; set; }
        public string nombre_1 { get; set; }
        public string nombre_2 { get; set; }
        public string apellido_1 { get; set; }
        public string apellido_2 { get; set; }
        public bool admin  { get; set; }
        public string password { get; set; }
        public string token { get; set; }


    }
}
