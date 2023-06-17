namespace SimplexInvoice.Domain.Products.Validations;

public class UpdateProductValidator : ProductValidator
{
    public UpdateProductValidator()
    {
        ValidateName();
        ValidateDescription();
        ValidateUnitPrice();
        ValidateProductTaxRateId();
    }

}
