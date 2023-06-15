namespace Invoice.Domain.TaxRates;
public partial class TaxRate
{
    public string Name { get; private set; } = string.Empty;
    public int Value { get; private set; } = 0;
}