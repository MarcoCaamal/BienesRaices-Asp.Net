using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BaseCore.Validaciones
{
    public class SoloNumerosAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var regex = new Regex(@"^\d*$");

            if (!regex.IsMatch(value.ToString()))
            {
                return new ValidationResult($"El campo {validationContext.DisplayName} solo acepta valores númericos.");
            }

            return ValidationResult.Success;
        }
    }
}
