using FluentValidation;

namespace SimplexInvoice.Domain.Companies.Validations;
public class CompanyValidator : AbstractValidator<Company>
{
    public void ValidateName()
    {
        RuleFor(c => c.Name).NotEmpty()
                            .Length(1, 40);
    }

    public void ValidateIdDocumentTypeId()
    {
        RuleFor(c => c.IdDocumentTypeId).NotEmpty();
    }

    public void ValidateIdDocumentNumber()
    {
        RuleFor(c => c.IdDocumentNumber).NotEmpty()
                                        .Length(1, 40);
    }

    public void ValidateEmail()
    {
        RuleFor(c => c.EmailAddress.Address).NotEmpty()
                                            .Length(1, 40);
    }
    
}