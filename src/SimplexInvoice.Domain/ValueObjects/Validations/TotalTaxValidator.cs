using FluentValidation;

namespace SimplexInvoice.Domain.ValueObjects.Validations;
public class TotalTaxValidator : AbstractValidator<TotalTax>
{
    public TotalTaxValidator()
    {
        ValidateName();
        ValidateAmount();
    }

    public void ValidateName()
    {
        RuleFor(c => c.Name).NotEmpty()
                            .Length(1, 20);
    }

    public void ValidateAmount()
    {
        RuleFor(c => c.Amount).NotNull();
    }

}