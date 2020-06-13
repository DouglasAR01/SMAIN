using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServicio.Entities
{
    public class Cuenta
    {
        [Key]
        public ulong id { get; set; }
        public decimal balance { get; set; }
        public string cedula { get; set; }
    }
}
