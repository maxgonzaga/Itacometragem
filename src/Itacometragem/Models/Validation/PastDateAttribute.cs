using System;
using System.ComponentModel.DataAnnotations;

namespace Itacometragem.Models
{
    public class PastDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string errorMessage;
            if (value is DateTime dateToCheck)
            {
                if (dateToCheck <= DateTime.Now)
                {
                    return ValidationResult.Success;
                }

                errorMessage = base.ErrorMessage ?? $"A data não pode estar no futuro.";
                return new ValidationResult(errorMessage);
            }
            errorMessage = base.ErrorMessage ?? $"Insira uma data válida no passado.";
            return new ValidationResult(errorMessage);
        }
    }
}
