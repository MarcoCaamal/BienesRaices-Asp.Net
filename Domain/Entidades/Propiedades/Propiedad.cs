using BaseCore.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.Propiedades
{
    public class Propiedad
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Titulo { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal? Precio { get; set; }
        public string Imagen { get; set; } = string.Empty;
        [LongitudStringMinima(logitudMinima: 50)]
        public string Descripcion { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, 9, ErrorMessage = "El campo {0} solo acepta valores de {1} al {2}.")]
        public int? Habitaciones { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, 9, ErrorMessage = "El campo {0} solo acepta valores de {1} al {2}.")]
        [Display(Name = "Baños")]
        public int? Wc { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, 9, ErrorMessage = "El campo {0} solo acepta valores de {1} al {2}.")]
        public int? Estacionamiento { get; set; }
        public DateTime Creado { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Vendedor")]
        public int? VendedorId { get; set; }
    }
}
