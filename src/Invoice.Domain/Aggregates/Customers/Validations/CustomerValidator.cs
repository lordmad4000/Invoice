using FluentValidation;

namespace Invoice.Domain.Customers.Validations;
public class CustomerValidator : AbstractValidator<Customer>
{
    public void ValidateFirstName()
    {
        RuleFor(c => c.FirstName).NotEmpty()
                                 .NotNull()
                                 .Length(1, 40);
    }

    public void ValidateLastName()
    {
        RuleFor(c => c.LastName).NotEmpty()
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