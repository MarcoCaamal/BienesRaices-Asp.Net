using System.ComponentModel.DataAnnotations;

namespace BaseCore.Validaciones
{
    public class LongitudStringMinimaAttribute: ValidationAttribute
    {
        private int LongitudMinima { get; set; }

        public LongitudStringMinimaAttribute(int logitudMinima)
        {
            LongitudMinima = logitudMinima;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var longitud = value.ToString().Length;

            if(longitud < this.LongitudMinima)
            {
                return new ValidationResult($"La longitud del texto debe tener un minimo de {this.LongitudMinima}");
            }

            return ValidationResult.Success;
        }
    }
}
