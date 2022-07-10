using FluentValidation.Results;

namespace Invoice.Application.Interfaces
{
    public interface IValidatorService
    {
        void ValidateModel(ValidationResult validator);
    }
}