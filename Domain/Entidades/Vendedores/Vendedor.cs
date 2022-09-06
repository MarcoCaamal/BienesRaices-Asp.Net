using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.Vendedores
{
    public class Vendedor
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Phone(ErrorMessage = "")]
        public string Telefono { get; set; }
    }
}
