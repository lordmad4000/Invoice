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
                                        double quantity,
                                        double price,
                                        string currency,
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
                              quantity,
                              price,
                              currency,
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
                       double quantity,
                       double price,
                       string currency,
                       string taxName,
                       int taxRate,
                       int discountRate)
    {
        InvoiceId = invoiceId;
        LineNumber = lineNumber;
        ProductCode = productCode;
        ProductName = productName;
        ProductDescription = productDescription;
        Quantity = quantity;
        Price = new Money(currency, price);
        TaxName = taxName;
        TaxRate = taxRate;
        DiscountRate = discountRate;

        ValidationResult validator = new UpdateInvoiceLineValidator().Validate(this);
        if (!validator.IsValid)
            throw new BusinessRuleValidationException(
                string.Join(", ", validator.Errors.Select(x => x.ErrorMessage).ToArray()));

        Quantity = Math.Round(quantity, QuantityRoundDigits);
        CalculateAmounts(currency);
    }

    private void CalculateAmounts(string currency)
    {
        TaxAmount = new Money(currency, CalculateTaxAmount());
        DiscountAmount = new Money(currency, CalculateDiscountAmount());
        TaxBaseAmount = new Money(currency, CalculateTaxBaseAmount());
        TotalAmount = new Money(currency, CalculateTotalAmount());
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
               $"Quantity: {Quantity}, " +
               $"Price: {Price.Amount}, " +
               $"TaxName: {TaxName}, " +
               $"TaxRate: {TaxRate}, " +
               $"DiscountRate: {DiscountRate}, " +
               $"TaxAmount: {TaxAmount.Amount}, " +
               $"DiscountAmount: {DiscountAmount.Amount}, " +
               $"TaxBaseAmount: {TaxBaseAmount.Amount}, " +
               $"TotalAmount: {TotalAmount.Amount}" +
               $"Currency: {Price.Currency}";
    }

}
