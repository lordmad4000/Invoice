using System;
using Invoice.Domain.Base;
using Invoice.Domain.ValueObjects;

namespace Invoice.Domain.Entities
{
    public partial class Company : IAggregateRoot
    {
        public Company()
        {            
        }

        public Company(string name, Guid idDocumentType, string idDocumentNumber, string address1, string address2, string city, string country, int postalCode, int phone, EmailAddress emailAddress)
        {
            Id = Guid.NewGuid();
            Update(name, idDocumentType, idDocumentNumber, address1, address2, city, country, postalCode, phone, emailAddress);
        }

        public void Update(string name, Guid idDocumentType, string idDocumentNumber, string address1, string address2, string city, string country, int postalCode, int phone, EmailAddress emailAddress)
        {
            Name = name;
            IdDocumentType = idDocumentType;
            IdDocumentNumber = idDocumentNumber;
            Address1 = address1;
            Address2 = address2;
            City = city;
            Country = country;
            PostalCode = postalCode;
            Phone = phone;
            EmailAddress = emailAddress;
        }
    }
}