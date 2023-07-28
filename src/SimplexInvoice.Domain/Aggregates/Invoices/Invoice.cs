using SimplexInvoice.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace SimplexInvoice.Domain.Invoices;
public partial class Invoice
{
    private readonly HashSet<InvoiceLine> _invoiceLines = new HashSet<InvoiceLine>();
    private readonly HashSet<TotalTax> _totalTaxes = new HashSet<TotalTax>();
    public IReadOnlyList<InvoiceLine> InvoiceLines => _invoiceLines.ToList();
    public IReadOnlyList<TotalTax> TotalTaxes => _totalTaxes.ToList();
    public string Number { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string CompanyName { get; private set; } = string.Empty;
    public string CompanyIdDocumentType { get; private set; } = string.Empty;
    public string CompanyDocumentNumber { get; private set; } = string.Empty;
    public Address CompanyAddress { get; private set; } = new Address("12, Black Cat St.", "Downtown", "Los Santos", "USA", "90210");
    public PhoneNumber CompanyPhoneNumber { get; private set; } = new PhoneNumber("+34 689 45 96 34");
    public EmailAddress CompanyEmailAddress { get; private set; } = new EmailAddress("defaultcompany@company.com");
    public string CustomerFullName { get; private set; } = string.Empty;
    public string CustomerIdDocumentType { get; private set; } = string.Empty;
    public string CustomerDocumentNumber { get; private set; } = string.Empty;
    public Address CustomerAddress { get; private set; } = new Address("12, Black Cat St.", "Downtown", "Los Santos", "USA", "90210");
    public PhoneNumber CustomerPhoneNumber { get; private set; } = new PhoneNumber("+34 689 45 96 34");
    public EmailAddress CustomerEmailAddress { get; private set; } = new EmailAddress("defaultcompany@company.com");
    public Money TotalTax { get; private set; } = new Money("USD", 0.0);
    public Money TotalDiscount { get; private set; } = new Money("USD", 0.0);
    public Money TotalTaxBase { get; private set; } = new Money("USD", 0.0);
    public Money Total { get; private set; } = new Money("USD", 0.0);
}