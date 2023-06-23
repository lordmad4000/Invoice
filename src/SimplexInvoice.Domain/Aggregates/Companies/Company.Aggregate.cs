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
                                 string phone,
                                 string emailAddress)
    {
        var company = new Company(Guid.NewGuid());
        company.Update(name, idDocumentTypeId, idDocumentNumber, address, phone, emailAddress);

        return company;
    }

    public void Update(string name,
                       Guid idDocumentTypeId,
                       string idDocumentNumber,
                       Address address,
                       string phone,
                       string emailAddress)
    {
        Name = name;
        IdDocumentTypeId = idDocumentTypeId;
        IdDocumentNumber = idDocumentNumber;
        CompanyAddress = address;
        Phone = new PhoneNumber(phone);
        EmailAddress = new EmailAddress(emailAddress);

        ValidationResult validator = new UpdateCompanyValidator().Validate(this);
        if (!validator.IsValid)
            throw new BusinessRuleValidationException(
                string.Join(", ", validator.Errors.Select(x => x.ErrorMessage).ToArray()));
    }

    public static Company Create(
        string name,
        Guid idDocumentTypeId,
        string idDocumentNumber,
        string street,
        string city,
        string country,
        string postalCode,
        string phone,
        string emailAddress) =>
            Create(name, idDocumentTypeId, idDocumentNumber, new Address(street, city, country, postalCode), phone, emailAddress);

    public void Update(
        string name,
        Guid idDocumentTypeId,
        string idDocumentNumber,
        string street,
        string city,
        string country,
        string postalCode,
        string phone,
        string emailAddress) =>
            Update(name, idDocumentTypeId, idDocumentNumber, new Address(street, city, country, postalCode), phone, emailAddress);
    public override string ToString()
    {
        return $"Id: {Id}, " +
               $"Name: {Name}, " +
               $"IdDocumentTypeId: {IdDocumentTypeId}, " +
               $"IdDocumentNumber: {IdDocumentNumber} ," +
               $"Street: {CompanyAddress.Street}, " +
               $"City: {CompanyAddress.City}, " +
               $"Country: {CompanyAddress.Country}, " +
               $"Postal Code: {CompanyAddress.PostalCode}, " +
               $"Phone: {Phone.Phone}, " +
               $"EmailAddress: {EmailAddress.Address}";
    }

}