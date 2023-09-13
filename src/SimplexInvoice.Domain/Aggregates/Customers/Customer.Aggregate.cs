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
                                  PhoneNumber phoneNumber,
                                  EmailAddress emailAddress)
    {
        var customer = new Customer(Guid.NewGuid());
        customer.Update(firstName, lastName, idDocumentTypeId, idDocumentNumber, address, phoneNumber, emailAddress);

        return customer;
    }

    public void Update(string firstName,
                       string lastName,
                       Guid idDocumentTypeId,
                       string idDocumentNumber,
                       Address address,
                       PhoneNumber phoneNumber,
                       EmailAddress emailAddress)
    {
        FirstName = firstName;
        LastName = lastName;
        IdDocumentTypeId = idDocumentTypeId;
        IdDocumentNumber = idDocumentNumber;
        Address = address;
        Phone = phoneNumber;
        EmailAddress = emailAddress;

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
               $"Street: {Address.Street}, " +
               $"City: {Address.City}, " +
               $"State: {Address.State}, " +
               $"Country: {Address.Country}, " +
               $"Postal Code: {Address.PostalCode}, " +
               $"Phone: {Phone.Phone}, " +
               $"EmailAddress: {EmailAddress.Address}";
    }

}