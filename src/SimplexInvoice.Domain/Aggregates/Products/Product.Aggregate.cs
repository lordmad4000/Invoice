using FluentValidation.Results;
using SimplexInvoice.Domain.Base;
using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.Products.Validations;
using SimplexInvoice.Domain.ValueObjects;
using System.Linq;
using System;

namespace SimplexInvoice.Domain.Products;
public partial class Product : AggregateRoot
{
    private Product(Guid id) : base(id)
    {
    }

    public static Product Create(string code,
                                 string name,
                                 string description,
                                 double packageQuantity,
                                 Money unitPrice,
                                 Guid taxRateId)
    {
        var product = new Product(Guid.NewGuid());
        product.Update(code,
                       name,
                       description,
                       packageQuantity,
                       unitPrice,
                       taxRateId);

        return product;
    }

    public void Update(string code,
                       string name,
                       string description,
                       double packageQuantity,
                       Money unitPrice,
                       Guid taxRateId)
    {
        Code = code;
        Name = name;
        Description = description;
        PackageQuantity = packageQuantity;
        UnitPrice = unitPrice;
        TaxRateId = taxRateId;
        TaxRate = null;

        ValidationResult validator = new UpdateProductValidator().Validate(this);
        if (!validator.IsValid)
            throw new BusinessRuleValidationException(
                string.Join(", ", validator.Errors.Select(x => x.ErrorMessage).ToArray()));
    }

    public override string ToString()
    {
        return $"Id: {Id}, " +
               $"Code: {Code}, " +
               $"Name: {Name}, " +
               $"Description: {Description}, " +
               $"PackageQuantity: {PackageQuantity}, " +
               $"UnitPrice: {UnitPrice.Amount}, " +
               $"Currency: {UnitPrice.Currency}, " +
               $"TaxRateId: {TaxRateId}";
    }

}