using System.ComponentModel.DataAnnotations;

namespace MicroServicio.Validators
{
    public class DatosTransferir 
    {
        [Required(ErrorMessage = "El numero de cuenta de origen es requerido.")]
        [Display(Name = "Numero de cuenta de origen")]
        public ulong? numCuentaOrigen { get; set; }

        [Required(ErrorMessage = "El numero de cuenta de destino es requerido.")]
        [Display(Name = "Numero de cuenta de destino")]
        public ulong? numCuentaDestino { get; set; }

        [Required(ErrorMessage = "El nombre de usuario destino es requerido.")]
        [StringLength(125, ErrorMessage = "El nombre no puede superar los 125 caracteres.")]
        [Display(Name = "Nombre de cuenta de destino")]
        public string nameDestino { get; set; }

        [Required(ErrorMessage = "El monto es requerido.")]
        [Display(Name = "Monto")]
        public decimal? monto { get;set; }
        
    }
}
