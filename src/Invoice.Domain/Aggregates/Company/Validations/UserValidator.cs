using FluentValidation;
using Invoice.Domain.Companies;

namespace Invoice.Domain.Companiess.Validations;
public class CompaniesValidator : AbstractValidator<Company>
{
    public void ValidateId()
    {
        RuleFor(c => c.Id).NotEmpty()
                          .NotNull();
    }

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

    public void ValidateEmail()
    {
        RuleFor(c => c.EmailAddress.Address).NotEmpty()
                                            .NotNull()
                                            .Length(1, 40);
    }

    // public void ValidatePassword()
    // {
    //     RuleFor(c => c.Password).NotEmpty()
    //                             .NotNull();
    // }


    // public void ValidateLastName()
    // {
    //     RuleFor(c => c.LastName).NotEmpty()
    //                             .NotNull()
    //                             .Length(1, 20);
    // }

}