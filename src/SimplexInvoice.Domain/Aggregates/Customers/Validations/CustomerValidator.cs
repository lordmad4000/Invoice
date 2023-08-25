using FluentValidation;

namespace SimplexInvoice.Domain.Customers.Validations;
public class CustomerValidator : AbstractValidator<Customer>
{
    public void ValidateFirstName()
    {
        RuleFor(c => c.FirstName).NotEmpty()
                                 .Length(1, 40);
    }

    public void ValidateLastName()
    {
        RuleFor(c => c.LastName).NotEmpty()
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