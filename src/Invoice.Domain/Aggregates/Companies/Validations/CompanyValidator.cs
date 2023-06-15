using FluentValidation;

namespace Invoice.Domain.Companies.Validations;
public class CompanyValidator : AbstractValidator<Company>
{
    public void ValidateName()
    {
        RuleFor(c => c.Name).NotEmpty()
                            .NotNull()
                            .Length(1, 40);
    }

    public void ValidateIdDocumentTypeId()
    {
        RuleFor(c => c.IdDocumentTypeId).NotEmpty()
                                        .NotNull();
    }

    public void ValidateIdDocumentNumber()
    {
        RuleFor(c => c.IdDocumentNumber).NotEmpty()
                                        .NotNull()
                                        .Length(1, 40);
    }

    public void ValidatePhone()
    {
        RuleFor(c => c.Phone.Phone).NotEmpty()
                                   .NotNull()
                                   .Length(1, 40);
    }

    public void ValidateEmail()
    {
        RuleFor(c => c.EmailAddress.Address).NotEmpty()
                                            .NotNull()
                                            .Length(1, 40);
    }
    
}