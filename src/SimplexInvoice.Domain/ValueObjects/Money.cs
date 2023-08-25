using FluentValidation.Results;
using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.ValueObjects.Validations;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SimplexInvoice.Domain.ValueObjects;
public class Money : ValueObject
{
    public const int AmountRoundDigits =  2;
    public string Currency { get; private set ; } = "USD";
    public double Amount { get; private set ; } = 0.0;

    public Money()
    {        
    }

    public Money(string currency, double amount)
    {
        Currency = currency;
        Amount = amount;
        Validate();
        Currency = currency.ToUpper();
        Amount = Math.Round(amount, AmountRoundDigits);
    }

    private void Validate()
    {
        ValidationResult validator = new MoneyValidator().Validate(this);
        if (!validator.IsValid)
            throw new NotValidMoneyException(
                string.Join(", ", validator.Errors.Select(x => x.ErrorMessage).ToArray()));
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Currency;
        yield return Amount;
    }
}
