using FluentValidation.Results;

namespace Users.Application.Interfaces
{
    public interface IValidatorService
    {
        void ValidateModel(ValidationResult validator);
    }
}