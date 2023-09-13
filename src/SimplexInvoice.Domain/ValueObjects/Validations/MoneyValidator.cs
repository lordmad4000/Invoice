using FluentValidation;

namespace SimplexInvoice.Domain.ValueObjects.Validations;

public class MoneyValidator : AbstractValidator<Money>
{
    public MoneyValidator()
    {
        ValidateCurrency();        
        ValidateAmount();
    }
    
    public void ValidateCurrency()
    {
        RuleFor(c => c.Currency).NotEmpty()
                                .NotNull()
                                .Length(3);
    }

    public void ValidateAmount()
    {
        RuleFor(c => c.Amount).NotNull();
    }

}