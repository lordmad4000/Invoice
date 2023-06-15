using FluentValidation.Results;
using Invoice.Domain.Base;
using Invoice.Domain.Exceptions;
using Invoice.Domain.TaxRates.Validations;
using System.Linq;
using System;

namespace Invoice.Domain.TaxRates;
public partial class TaxRate : AggregateRoot
{
    private TaxRate(Guid id) : base(id)
    {
    }

    public static TaxRate Create(string name, int value)
    {
        var user = new TaxRate(Guid.NewGuid());
        user.Update(name, value);

        return user;
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