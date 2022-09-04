using BaseCore.Validaciones;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BaseCore.Entidades.ViewModels
{
    public class PropiedadCreacionVM
    {
        public Propiedad Propiedad { get; set; }
        [Display(Name = "Imagen")]
        [TipoArchivoValidacion(GrupoTipoArchivo.Imagen)]
        [PesoArchivoValidacion(PesoMaximoEnMegaByte: 5)]
        public IFormFile ImagenPropiedad { get; set; }
        public IEnumerable<SelectListItem> Vendedores { get; set; }
    }
}
