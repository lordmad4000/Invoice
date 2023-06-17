using SimplexInvoice.Domain.TaxRates;
using System;

namespace SimplexInvoice.Domain.Products;
public partial class Product
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public double UnitPrice { get; private set; } = 0.0;
    public Guid ProductTaxRateId { get; private set; } = Guid.Empty;
    public virtual TaxRate ProductTaxRate{ get; private set; } = TaxRate.Create("10", 10);
}