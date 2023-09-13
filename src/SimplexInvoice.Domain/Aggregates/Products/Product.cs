using SimplexInvoice.Domain.TaxRates;
using SimplexInvoice.Domain.ValueObjects;
using System;

namespace SimplexInvoice.Domain.Products;
public partial class Product
{
    public string Code { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public double PackageQuantity { get; private set; } = 1.0;
    public Money UnitPrice { get; private set; } = new Money("USD", 0.0);
    public Guid ProductTaxRateId { get; private set; } = Guid.Empty;
    public virtual TaxRate ProductTaxRate { get; private set; } = null;
}