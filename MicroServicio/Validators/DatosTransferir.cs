using System.ComponentModel.DataAnnotations;

namespace MicroServicio.Validators
{
    public class DatosTransferir 
    {
        [Required(ErrorMessage = "El numero de cuenta de origen es requerido.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El numero de cuenta de origen debe ser numerica y positiva.")]
        public ulong? numCuentaOrigen { get; set; }

        [Required(ErrorMessage = "El numero de cuenta de destino es requerido.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El numero de cuenta de destino debe ser numerica y positiva.")]
        public ulong? numCuentaDestino { get; set; }

        [Required(ErrorMessage = "El nombre de usuario destino es requerido.")]
        [StringLength(125, ErrorMessage = "El nombre no puede superar los 125 caracteres.")]
        public string nameDestino { get; set; }

        [Required(ErrorMessage = "El monto es requerido.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El monto debe ser numerico y positivo.")]
        public decimal? monto { get;set; }
        
    }
}
