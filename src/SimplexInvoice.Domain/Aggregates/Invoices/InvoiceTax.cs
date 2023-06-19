namespace SimplexInvoice.Domain.Invoices;
public partial class InvoiceTax
{
    public string Name { get; private set; } = string.Empty;    
    public double Total { get; private set; } = 0.0;
}