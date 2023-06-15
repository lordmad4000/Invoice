using FluentValidation;

namespace Invoice.Domain.IdDocumentTypes.Validations
{
    public class IdDocumentTypeValidator : AbstractValidator<IdDocumentType>
    {
        public void ValidateName()
        {
            RuleFor(c => c.Name).NotEmpty()
                                .NotNull()
                                .Length(1, 20);
        }

    }
}