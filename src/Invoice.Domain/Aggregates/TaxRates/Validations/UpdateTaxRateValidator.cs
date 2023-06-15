namespace Invoice.Domain.TaxRates.Validations;
public class UpdateTaxRateValidator : TaxRateValidator
{
    public UpdateTaxRateValidator()
    {
        ValidateName();
        ValidateValue();
    }

}