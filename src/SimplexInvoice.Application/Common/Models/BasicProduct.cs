using System;

namespace SimplexInvoice.Application.Common.Models;
public class BasicProduct
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; } = 0.0;
    public string Currency { get; set; } = "USD";
    public int TaxRateValue { get; set; } = 0;
}