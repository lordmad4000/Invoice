using FluentValidation.Results;
using SimplexInvoice.Domain.Base;
using SimplexInvoice.Domain.Customers.Validations;
using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.ValueObjects;
using System.Linq;
using System;

namespace SimplexInvoice.Domain.Customers;
public partial class Customer : AggregateRoot
{
    private Customer(Guid id) : base(id)
    {
    }

    public static Customer Create(string firstName,
                                  string lastName,
                                  Guid idDocumentTypeId,
                                  string idDocumentNumber,
                                  Address address,
                                  string phone,
                                  string emailAddress)
    {
        var customer = new Customer(Guid.NewGuid());
        customer.Update(firstName, lastName, idDocumentTypeId, idDocumentNumber, address, phone, emailAddress);

        return customer;
    }

    public void Update(string firstName,
                       string lastName,
                       Guid idDocumentTypeId,
                       string idDocumentNumber,
                       Address address,
                       string phone,
                       string emailAddress)
    {
        FirstName = firstName;
        LastName = lastName;
        IdDocumentTypeId = idDocumentTypeId;
        IdDocumentNumber = idDocumentNumber;
        CustomerAddress = address;
        Phone = new PhoneNumber(phone);
        EmailAddress = new EmailAddress(emailAddress);

        ValidationResult validator = new UpdateCustomerValidator().Validate(this);
        if (!validator.IsValid)
            throw new BusinessRuleValidationException(
                string.Join(", ", validator.Errors.Select(x => x.ErrorMessage).ToArray()));
    }

    public static Customer Create(
        string firstName,
        string lastName,
        Guid idDocumentTypeId,
        string idDocumentNumber,
        string street,
        string city,
        string country,
        string postalCode,
        string phone,
        string emailAddress) =>
            Create(firstName, lastName, idDocumentTypeId, idDocumentNumber, new Address(street, city, country, postalCode), phone, emailAddress);

    public void Update(
        string firstName,
        string lastName,
        Guid idDocumentTypeId,
        string idDocumentNumber,
        string street,
        string city,
        string country,
        string postalCode,
        string phone,
        string emailAddress) =>
            Update(firstName, lastName, idDocumentTypeId, idDocumentNumber, new Address(street, city, country, postalCode), phone, emailAddress);
    
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