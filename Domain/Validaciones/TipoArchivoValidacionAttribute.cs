using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BaseCore.Validaciones
{
    public class TipoArchivoValidacionAttribute: ValidationAttribute
    {
        private readonly string[] _tiposValidos;

        public TipoArchivoValidacionAttribute(string[] tiposValidos)
        {
            _tiposValidos = tiposValidos;
        }

        public TipoArchivoValidacionAttribute(GrupoTipoArchivo grupoTipoArchivo)
        {
            if (grupoTipoArchivo == GrupoTipoArchivo.Imagen)
            {
                _tiposValidos = new string[] { "image/jpg", "image/jpeg", "image/webp", "image/gif", "image/png" };
            }
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return ValidationResult.Success;
            }

            IFormFile formFile = value as IFormFile;//Conversion del object

            if (formFile is null)
            {
                return ValidationResult.Success;
            }

            if (!_tiposValidos.Contains(formFile.ContentType))
            {
                return new ValidationResult($"El tipo de archivo debe ser uno de los siguiente: {string.Join(", ",_tiposValidos)}");
            }

            return ValidationResult.Success;
        }
    }
}
