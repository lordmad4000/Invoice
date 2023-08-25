using FluentValidation.Results;
using SimplexInvoice.Domain.Base;
using SimplexInvoice.Domain.Companies.Validations;
using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.ValueObjects;
using System.Linq;
using System;

namespace SimplexInvoice.Domain.Companies;
public partial class Company : AggregateRoot
{
    private Company(Guid id) : base(id)
    {
    }

    public static Company Create(string name,
                                 Guid idDocumentTypeId,
                                 string idDocumentNumber,
                                 Address address,
                                 PhoneNumber phoneNumber,
                                 EmailAddress emailAddress)
    {
        var company = new Company(Guid.NewGuid());
        company.Update(name, idDocumentTypeId, idDocumentNumber, address, phoneNumber, emailAddress);

        return company;
    }

    public void Update(string name,
                       Guid idDocumentTypeId,
                       string idDocumentNumber,
                       Address address,
                       PhoneNumber phoneNumber,
                       EmailAddress emailAddress)
    {
        Name = name;
        IdDocumentTypeId = idDocumentTypeId;
        IdDocumentNumber = idDocumentNumber;
        Address = address;
        Phone = phoneNumber;
        EmailAddress = emailAddress;

        ValidationResult validator = new UpdateCompanyValidator().Validate(this);
        if (!validator.IsValid)
            throw new BusinessRuleValidationException(
                string.Join(", ", validator.Errors.Select(x => x.ErrorMessage).ToArray()));
    }

    public override string ToString()
    {
        return $"Id: {Id}, " +
               $"Name: {Name}, " +
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