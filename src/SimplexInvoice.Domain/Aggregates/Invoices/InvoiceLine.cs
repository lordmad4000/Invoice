using System;

namespace SimplexInvoice.Domain.Invoices;
public partial class InvoiceLine
{
    public Guid InvoiceId { get; private set; } = Guid.Empty;
    public int LineNumber { get; private set; } = 0;
    public string ProductCode { get; private set; } = string.Empty;
    public string ProductName { get; private set; } = string.Empty;
    public string ProductDescription { get; private set; } = string.Empty;
    public double Quantity { get; private set; } = 0;
    public double Price { get; private set; } = 0.0;
    public string TaxName { get; private set; } = string.Empty;
    public int TaxRate { get; private set; } = 0;
    public int DiscountRate { get; private set; } = 0;
    public double TaxAmount { get; private set; } = 0.0;
    public double DiscountAmount { get; private set; } = 0.0;
    public double TaxBaseAmount { get; private set; } = 0.0;
    public double TotalAmount { get; private set; } = 0.0;
}