using SimplexInvoice.Domain.IdDocumentTypes;
using SimplexInvoice.Domain.ValueObjects;
using System;

namespace SimplexInvoice.Domain.Companies;
public partial class Company
{
    public string Name { get; private set; } = string.Empty;
    public Guid IdDocumentTypeId { get; private set; } = Guid.Empty;
    public string IdDocumentNumber { get; private set; } = string.Empty;
    public Address Address { get; private set; } = new Address("12, Black Cat St.", "Downtown", "Los Santos", "USA", "90210");
    public PhoneNumber Phone { get; private set; } = new PhoneNumber("+34 689 45 96 34");
    public EmailAddress EmailAddress { get; private set; } = new EmailAddress("defaultcompany@company.com");
    public virtual IdDocumentType? IdDocumentType { get; private set; } = null;
}