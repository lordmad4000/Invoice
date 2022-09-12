using System.Text;
using FluentValidation.Results;
using Invoice.Application.Interfaces;
using Invoice.Domain.Exceptions;

namespace Invoice.Application.Services
{

    public class ValidatorService : IValidatorService
    {

        public void ValidateModel(ValidationResult validator)
        {
            if (!validator.IsValid)
            {
                var errors = new StringBuilder();

                foreach (var error in validator.Errors)
                    errors.AppendLine(error.ErrorMessage);

                throw new EntityValidationException(errors.ToString());
            }
        }

    }
}