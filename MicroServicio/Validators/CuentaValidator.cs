using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServicio.Validators
{
    public class CuentaValidator
    {
        [Required(ErrorMessage = "El numero de cuenta a editar el balance es requerido.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El numero de cuenta debe ser numerica y positiva.")]
        public ulong? numCuenta { get; set; }
        [Required(ErrorMessage = "El monto a modificar es requerido.")]
        [RegularExpression(@"^\d+.{0,1}\d+$", ErrorMessage = "El monto debe ser numerico y positivo.")]
        public decimal? nuevoBalance { get; set; }
    }
}
