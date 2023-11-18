using FluentValidation.Results;
using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.ValueObjects.Validations;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SimplexInvoice.Domain.ValueObjects;
public class TotalTax : ValueObject
{
    public const int AmountRoundDigits = 2;
    public string Name { get; private set; } = string.Empty;
    public double BaseAmount { get; private set; } = 0.0;
    public double Amount { get; private set; } = 0.0;

    private TotalTax()
    {
    }

    public TotalTax(string name, double baseAmount, double amount)
    {
        Name = name;
        Amount = amount;
        BaseAmount = baseAmount;
        Validate();
        Name = name.ToUpper();
        Amount = Math.Round(amount, AmountRoundDigits);
        BaseAmount = Math.Round(baseAmount, AmountRoundDigits);
    }

    private void Validate()
    {
        ValidationResult validator = new TotalTaxValidator().Validate(this);
        if (!validator.IsValid)
            throw new NotValidTotalTaxException(
                string.Join(", ", validator.Errors.Select(x => x.ErrorMessage).ToArray()));
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Name;
        yield return BaseAmount;
        yield return Amount;
    }

}