using FluentValidation;

namespace Invoice.Domain.Products.Validations;
public class ProductValidator : AbstractValidator<Product>
{
    public void ValidateName()
    {
        RuleFor(c => c.Name).NotEmpty()
                            .NotNull()
                            .Length(1, 40);
    }

    public void ValidateDescription()
    {
        RuleFor(c => c.Description).NotEmpty()
                                   .NotNull()
                                   .Length(1, 40);
    }

    public void ValidateUnitPrice()
    {
        RuleFor(c => c.UnitPrice).NotNull()
                                 .GreaterThanOrEqualTo(0.0);
    }

    public void ValidateProductTaxRateId()
    {
        RuleFor(c => c.ProductTaxRateId).NotEmpty()
                                        .NotNull();
    }
   
}