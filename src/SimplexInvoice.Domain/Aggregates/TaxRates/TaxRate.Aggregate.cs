using FluentValidation.Results;
using SimplexInvoice.Domain.Base;
using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.TaxRates.Validations;
using System.Linq;
using System;

namespace SimplexInvoice.Domain.TaxRates;
public partial class TaxRate : AggregateRoot
{
    private TaxRate(Guid id) : base(id)
    {
    }

    public static TaxRate Create(string name, int value)
    {
        var taxRate = new TaxRate(Guid.NewGuid());
        taxRate.Update(name, value);

        return taxRate;
    }

    public void Update(string name, int value)
    {
        Name = name;
        Value = value;

        ValidationResult validator = new UpdateTaxRateValidator().Validate(this);
        if (!validator.IsValid)
            throw new BusinessRuleValidationException(
                string.Join(", ", validator.Errors.Select(x => x.ErrorMessage).ToArray()));
    }

    public override string ToString()
    {
        return $"Id: {Id}, " +
               $"Name: {Name}, " +
               $"Value: {Value}";
    }

}