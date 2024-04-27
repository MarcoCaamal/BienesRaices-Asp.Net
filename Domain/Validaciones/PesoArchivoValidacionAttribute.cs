using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BaseCore.Validaciones
{
    public class PesoArchivoValidacionAttribute: ValidationAttribute
    {
        private readonly int _pesoMaximoEnMegaByte;

        public PesoArchivoValidacionAttribute(int PesoMaximoEnMegaByte)
        {
            _pesoMaximoEnMegaByte = PesoMaximoEnMegaByte;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if(value is null)
            {
                return ValidationResult.Success!;
            }

            IFormFile? formFile = value as IFormFile;//Conversion del object

            if (formFile is null)
            {
                return ValidationResult.Success!;
            }

            if(formFile.Length > _pesoMaximoEnMegaByte * 1024 * 1024)
            {
                return new ValidationResult($"El peso del archivo no debe ser mayor a {this._pesoMaximoEnMegaByte}mb.");
            }

            return ValidationResult.Success!;
        }
    }
}
