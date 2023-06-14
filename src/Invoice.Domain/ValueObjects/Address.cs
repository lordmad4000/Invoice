using FluentValidation.Results;
using Invoice.Domain.Exceptions;
using Invoice.Domain.ValueObjects.Validations;
using System.Collections.Generic;
using System.Linq;

namespace Invoice.Domain.ValueObjects;

public class Address : ValueObject
{
    public string Street { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string Country { get; private set; } = string.Empty;
    public string PostalCode { get; private set; } = string.Empty;

    public Address(string street, string city, string country, string postalCode)
    {
        Street = street;
        City = city;
        Country = country;
        PostalCode = postalCode;
        Validate();
    }

    private void Validate()
    {
        ValidationResult validator = new AddressValidator().Validate(this);
        if (!validator.IsValid)
            throw new NotValidAddressException(
                string.Join(", ", validator.Errors.Select(x => x.ErrorMessage).ToArray()));
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Street;
        yield return City;
        yield return Country;
        yield return PostalCode;
    }

    public override string ToString()
    {
        return $"{Street}, {City}, {Country}, {PostalCode}";
    }

}