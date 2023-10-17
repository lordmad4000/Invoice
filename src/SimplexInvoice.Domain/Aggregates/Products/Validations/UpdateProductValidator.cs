namespace SimplexInvoice.Domain.Products.Validations;

public class UpdateProductValidator : ProductValidator
{
    public UpdateProductValidator()
    {
        ValidateCode();
        ValidateName();
        ValidateDescription();
        ValidatePackageQuantity();
        ValidateUnitPrice();
        ValidateTaxRateId();
    }

}
