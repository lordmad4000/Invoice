using FluentValidation.Results;
using SimplexInvoice.Domain.Base;
using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.Invoices.Validations;
using SimplexInvoice.Domain.ValueObjects;
using System.Linq;
using System;

namespace SimplexInvoice.Domain.Invoices;
public partial class InvoiceLine : Aggregate
{
    public const int QuantityRoundDigits = 3;
    private InvoiceLine(Guid id) : base(id) { }

    public static InvoiceLine Create(Guid invoiceId,
                                     int lineNumber,
                                     string productCode,
                                     string productName,
                                     string productDescription,
                                     int packages,
                                     double quantity,
                                     Money price,
                                     string taxName,
                                     int taxRate,
                                     int discountRate)
    {
        var invoiceLine = new InvoiceLine(Guid.NewGuid());
        invoiceLine.Update(invoiceId,
                           lineNumber,
                           productCode,
                           productName,
                           productDescription,
                           packages,
                           quantity,
                           price,
                           taxName,
                           taxRate,
                           discountRate);

        return invoiceLine;
    }

    public void Update(Guid invoiceId,
                       int lineNumber,
                       string productCode,
                       string productName,
                       string productDescription,
                       int packages,
                       double quantity,
                       Money price,
                       string taxName,
                       int taxRate,
                       int discountRate)
    {
        InvoiceId = invoiceId;
        LineNumber = lineNumber;
        ProductCode = productCode;
        ProductName = productName;
        ProductDescription = productDescription;
        Packages = packages;
        Quantity = quantity;
        Price = price;
        TaxName = taxName;
        TaxRate = taxRate;
        DiscountRate = discountRate;

        ValidationResult validator = new UpdateInvoiceLineValidator().Validate(this);
        if (!validator.IsValid)
            throw new BusinessRuleValidationException(
                string.Join(", ", validator.Errors.Select(x => x.ErrorMessage).ToArray()));

        Quantity = Math.Round(quantity, QuantityRoundDigits);
        CalculateAmounts();
    }

    private void CalculateAmounts()
    {
        Tax = new Money(Price.Currency, CalculateTaxAmount());
        Discount = new Money(Price.Currency, CalculateDiscountAmount());
        TaxBase = new Money(Price.Currency, CalculateTaxBaseAmount());
        Total = new Money(Price.Currency, CalculateTotalAmount());
    }

    private double CalculateTaxAmount() =>
        CalculateTaxBaseAmount() * TaxRate / 100;

    private double CalculateDiscountAmount() =>
        CalculateTaxBaseAmount() * DiscountRate / 100;

    private double CalculateTaxBaseAmount() =>
        Quantity * Price.Amount;

    private double CalculateTotalAmount() =>
        CalculateTaxBaseAmount() + CalculateTaxAmount() - CalculateDiscountAmount();

    public override string ToString()
    {
        return $"Id: {Id}, " +
               $"InvoiceId: {InvoiceId}, " +
               $"LineNumber: {LineNumber}, " +
               $"ProductCode: {ProductCode}, " +
               $"ProductName: {ProductName}, " +
               $"ProductDescription: {ProductDescription}, " +
               $"Packages: {Packages}, " +
               $"Quantity: {Quantity}, " +
               $"Price: {Price.Amount}, " +
               $"Currency: {Price.Currency}, " +
               $"TaxName: {TaxName}, " +
               $"TaxRate: {TaxRate}, " +
               $"DiscountRate: {DiscountRate}, " +
               $"TaxAmount: {Tax.Amount}, " +
               $"DiscountAmount: {Discount.Amount}, " +
               $"TaxBaseAmount: {TaxBase.Amount}, " +
               $"TotalAmount: {Total.Amount}";
    }

}
