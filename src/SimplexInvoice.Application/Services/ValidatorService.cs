using System.Text;
using FluentValidation.Results;
using SimplexInvoice.Application.Interfaces;
using SimplexInvoice.Domain.Exceptions;

namespace SimplexInvoice.Application.Services
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

                throw new BusinessRuleValidationException(errors.ToString());
            }
        }

    }
}