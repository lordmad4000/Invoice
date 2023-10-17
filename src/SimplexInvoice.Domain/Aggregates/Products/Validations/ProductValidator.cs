using FluentValidation;

namespace SimplexInvoice.Domain.Products.Validations;
public class ProductValidator : AbstractValidator<Product>
{
    public void ValidateCode()
    {
        RuleFor(c => c.Code).NotEmpty()
                            .Length(1, 20);
    }

    public void ValidateName()
    {
        RuleFor(c => c.Name).NotEmpty()
                            .Length(1, 40);
    }

    public void ValidateDescription()
    {
        RuleFor(c => c.Description).NotEmpty()
                                   .Length(1, 40);
    }

    public void ValidatePackageQuantity()
    {
        RuleFor(c => c.PackageQuantity).NotNull()
                                       .GreaterThan(0.0);
    }

    public void ValidateUnitPrice()
    {
        RuleFor(c => c.UnitPrice.Amount).NotNull()
                                        .GreaterThanOrEqualTo(0.0);
    }

    public void ValidateTaxRateId()
    {
        RuleFor(c => c.TaxRateId).NotNull();
    }
   
}