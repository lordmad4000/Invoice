using FluentValidation.Results;

namespace SimplexInvoice.Application.Interfaces
{
    public interface IValidatorService
    {
        void ValidateModel(ValidationResult validator);
    }
}