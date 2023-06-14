using FluentValidation.Results;
using Invoice.Domain.Base;
using Invoice.Domain.Companies.Validations;
using Invoice.Domain.Exceptions;
using Invoice.Domain.ValueObjects;
using System.Linq;
using System;

namespace Invoice.Domain.Companies;
public partial class Company : AggregateRoot
{
    private Company(Guid id) : base(id)
    {
    }

    public static Company Create(string name, 
                                 Guid idDocumentTypeId, 
                                 string idDocumentNumber, 
                                 string street, 
                                 string city, 
                                 string country, 
                                 string postalCode, 
                                 string phone, 
                                 string emailAddress)
    {
        var company = new Company(Guid.NewGuid());

        company.Update(name, idDocumentTypeId, idDocumentNumber, street, city, country, postalCode, phone, emailAddress);

        return company;
    }

    public void Update(string name, 
                       Guid idDocumentTypeId, 
                       string idDocumentNumber, 
                       string street, 
                       string city, 
                       string country, 
                       string postalCode, 
                       string phone, 
                       string emailAddress)
    {
        Name = name;
        IdDocumentTypeId = idDocumentTypeId;
        IdDocumentNumber = idDocumentNumber;        
        CompanyAddress = new Address(street, city, country, postalCode);
        Phone = new PhoneNumber(phone);
        EmailAddress = new EmailAddress(emailAddress);

        ValidationResult validator = new CreateCompanyValidator().Validate(this);
        if (!validator.IsValid)
            throw new BusinessRuleValidationException(
                string.Join(", ", validator.Errors.Select(x => x.ErrorMessage).ToArray()));
    }
    
}