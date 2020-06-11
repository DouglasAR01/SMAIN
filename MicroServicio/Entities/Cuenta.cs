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
        public ulong Cta_id { get; set; }
        public decimal cta_balance { get; set; }
        public string fk_usr_cedula { get; set; }
    }
}
