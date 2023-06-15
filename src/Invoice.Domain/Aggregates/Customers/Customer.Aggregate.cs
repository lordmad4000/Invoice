using FluentValidation.Results;
using Invoice.Domain.Base;
using Invoice.Domain.Customers.Validations;
using Invoice.Domain.Exceptions;
using Invoice.Domain.ValueObjects;
using System.Linq;
using System;

namespace Invoice.Domain.Customers;
public partial class Customer : AggregateRoot
{
    private Customer(Guid id) : base(id)
    {
    }

    public static Customer Create(string firstName,
                                  string lastName,
                                  Guid idDocumentTypeId,
                                  string idDocumentNumber,
                                  string street,
                                  string city,
                                  string country,
                                  string postalCode,
                                  string phone,
                                  string emailAddress)
    {
        var customer = new Customer(Guid.NewGuid());
        customer.Update(firstName, lastName, idDocumentTypeId, idDocumentNumber, street, city, country, postalCode, phone, emailAddress);

        return customer;
    }

    public void Update(string firstName,
                       string lastName,
                       Guid idDocumentTypeId,
                       string idDocumentNumber,
                       string street,
                       string city,
                       string country,
                       string postalCode,
                       string phone,
                       string emailAddress)
    {
        FirstName = firstName;
        LastName = lastName;
        IdDocumentTypeId = idDocumentTypeId;
        IdDocumentNumber = idDocumentNumber;
        CustomerAddress = new Address(street, city, country, postalCode);
        Phone = new PhoneNumber(phone);
        EmailAddress = new EmailAddress(emailAddress);

        ValidationResult validator = new UpdateCustomerValidator().Validate(this);
        if (!validator.IsValid)
            throw new BusinessRuleValidationException(
                string.Join(", ", validator.Errors.Select(x => x.ErrorMessage).ToArray()));
    }
    
    public override string ToString()
    {
        return $"Id: {Id}, " +
               $"FirstName: {FirstName}, " +
               $"LastName: {LastName}, " +
               $"IdDocumentTypeId: {IdDocumentTypeId}, " +
               $"IdDocumentNumber: {IdDocumentNumber} ," +
               $"Street: {CustomerAddress.Street}, " +
               $"City: {CustomerAddress.City}, " +
               $"Country: {CustomerAddress.Country}, " +
               $"Postal Code: {CustomerAddress.PostalCode}, " +
               $"Phone: {Phone.Phone}, " +
               $"EmailAddress: {EmailAddress.Address}";
    }

}