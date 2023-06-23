using SimplexInvoice.Domain.ValueObjects;

namespace SimplexInvoice.Domain.Invoices;
public partial class InvoiceTax
{
    public string Name { get; private set; } = string.Empty;    
    public Money Total { get; private set; } = new Money("USD", 0.0);
}