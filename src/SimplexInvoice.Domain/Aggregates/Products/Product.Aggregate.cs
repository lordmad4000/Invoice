using FluentValidation.Results;
using SimplexInvoice.Domain.Base;
using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.Products.Validations;
using System.Linq;
using System;

namespace SimplexInvoice.Domain.Products;
public partial class Product : AggregateRoot
{
    private Product(Guid id) : base(id)
    {
    }

    public static Product Create(string name, string description, double unitPrice, Guid productTaxRateId)
    {
        var product = new Product(Guid.NewGuid());
        product.Update(name, description, unitPrice, productTaxRateId);

        return product;
    }

    public void Update(string name, string description, double unitPrice, Guid productTaxRateId)
    {
        Name = name;
        Description = description;
        UnitPrice = unitPrice;
        ProductTaxRateId = productTaxRateId;

        ValidationResult validator = new UpdateProductValidator().Validate(this);
        if (!validator.IsValid)
            throw new BusinessRuleValidationException(
                string.Join(", ", validator.Errors.Select(x => x.ErrorMessage).ToArray()));
    }

    public override string ToString()
    {
        return $"Id: {Id}, " +
               $"Name: {Name}, " +
               $"Description: {Description}, " +
               $"UnitPrice: {UnitPrice}, " +
               $"ProductTaxRateId: {ProductTaxRateId}";
    }

}