using System;
using Invoice.Domain.Base;
using Invoice.Domain.ValueObjects;

namespace Invoice.Domain.Entities
{
    public partial class Company : BaseEntity
    {
        public string Name { get; private set; }
        public Guid IdDocumentType { get; private set; }
        public string IdDocumentNumber { get; private set; }
        public string Address1 { get; private set; }
        public string Address2 { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        public int PostalCode { get; private set; }
        public int Phone { get; private set; }
        public EmailAddress EmailAddress { get; private set; }
        public virtual IdDocumentType DocumentType { get; private set; }
    }
}