using Invoice.Domain.IdDocumentTypes;
using Invoice.Domain.ValueObjects;
using System;

namespace Invoice.Domain.Customers;
public partial class Customer
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public Guid IdDocumentTypeId { get; private set; } = Guid.Empty;
    public string IdDocumentNumber { get; private set; } = string.Empty;
    public Address CustomerAddress { get; private set; } = new Address("12, Black Cat St.", "Downtown", "Los Santos", "90210");
    public PhoneNumber Phone { get; private set; } = new PhoneNumber("+34 689 45 96 34");
    public EmailAddress EmailAddress { get; private set; } = new EmailAddress("defaultcompany@company.com");
    public virtual IdDocumentType DocumentType { get; private set; } = IdDocumentType.Create("NIF");
}