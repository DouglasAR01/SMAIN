using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServicio.Entities
{
    public class Cuenta
    {
        [Key]
        [Required(ErrorMessage = "El numero de cuenta es requerido.")]
        public ulong? id { get; set; }

        [Required(ErrorMessage = "El balance es requerido.")]
        public decimal? balance { get; set;}

        [ForeignKey("Usuario")]
        public string cedula { get; set; }
        public Usuario Usuario { get; set; }
    }
}
