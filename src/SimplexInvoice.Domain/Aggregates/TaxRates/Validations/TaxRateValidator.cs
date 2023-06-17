using FluentValidation;

namespace SimplexInvoice.Domain.TaxRates.Validations;
public class TaxRateValidator : AbstractValidator<TaxRate>
{
    public void ValidateName()
    {
        RuleFor(c => c.Name).NotEmpty()
                            .NotNull()
                            .Length(1, 20);
    }

    public void ValidateValue()
    {
        RuleFor(c => c.Value).NotNull()
                             .GreaterThanOrEqualTo(0)
                             .LessThanOrEqualTo(100);
    }

}