using System.ComponentModel.DataAnnotations;

namespace MicroServicio.Validators
{
    public class DatosTransferir 
    {
        [Required(ErrorMessage = "El numero de cuenta de origen es requerido.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El numero de cuenta de origen debe ser un numero entero positivo.")]
        public string numCuentaOrigen { get; set; }

        [Required(ErrorMessage = "El numero de cuenta de destino es requerido.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El numero de cuenta de destino debe ser un numero entero positivo.")]
        public string numCuentaDestino { get; set; }

        [Required(ErrorMessage = "El nombre de usuario destino es requerido.")]
        [StringLength(125, ErrorMessage = "El nombre no puede superar los 123 caracteres.")]
        public string nameDestino { get; set; }

        [Required(ErrorMessage = "El monto es requerido.")]
        [RegularExpression(@"^\d+\.{0,1}\d+$", ErrorMessage = "El monto debe ser un numero positivo.")]
        public string monto { get;set; }
        
    }
}
