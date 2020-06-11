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
        public string Usr_cedula { get; set; }
        public string usr_nombre_1 { get; set; }
        public string usr_nombre_2 { get; set; }
        public string usr_apellido_1 { get; set; }
        public string usr_apellido_2 { get; set; }
        public bool usr_admin  { get; set; }

    }
}
