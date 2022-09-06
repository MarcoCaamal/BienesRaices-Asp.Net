using System.ComponentModel.DataAnnotations;
using static Common.Enums.Enums;

namespace Domain.Entidades.Contacto
{
    public class ContactoVM
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Nombre { get; set; }
        [EmailAddress(ErrorMessage = "El campo {0} no contiene un email valido.")]
        public string Email { get; set; }
        [Phone(ErrorMessage = "El campo {0} no contiene un número de teléfono valido.")]
        [StringLength(maximumLength: 10 ,ErrorMessage = "El número de teléfono no puede contener más de 10 caracteres.")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Mensaje { get; set; }
        [Required(ErrorMessage = "Debe de seleccionar una opción de vende y compra.")]
        public OpcionCompraVenta? OpcionVendeCompra { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public decimal? PrecioPresupuesto { get; set; }
        [Required(ErrorMessage = "La opción de contacto es requerida.")]
        public string OpcionContacto { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
    }
}
