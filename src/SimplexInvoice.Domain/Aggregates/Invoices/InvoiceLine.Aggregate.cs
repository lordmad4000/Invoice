using FluentValidation.Results;
using SimplexInvoice.Domain.Base;
using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.Invoices.Validations;
using System.Linq;
using System;

namespace SimplexInvoice.Domain.Invoices
{
    public partial class InvoiceLine : Aggregate
    {
        private InvoiceLine(Guid id) : base(id) { }

        public static InvoiceLine Create(Guid invoiceId,
                                            int lineNumber,
                                            string productCode,
                                            string productName,
                                            string productDescription,
                                            double quantity,
                                            double price,
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
                           string taxName,
                           int taxRate,
                           int discountRate)
        {
            InvoiceId = invoiceId;
            LineNumber = lineNumber;
            ProductCode = productCode;
            ProductName = productName;
            ProductDescription = productDescription;
            Quantity = Math.Round(quantity, 3);
            Price = Math.Round(price, 2);
            TaxName = taxName;
            TaxRate = taxRate;
            DiscountRate = discountRate;
            TaxAmount = CalculateTaxAmount();
            DiscountAmount = CalculateDiscountAmount();
            TaxBaseAmount = CalculateTaxBaseAmount();
            TotalAmount = CalculateTotalAmount();

            ValidationResult validator = new UpdateInvoiceLineValidator().Validate(this);
            if (!validator.IsValid)
                throw new BusinessRuleValidationException(
                    string.Join(", ", validator.Errors.Select(x => x.ErrorMessage).ToArray()));
        }

        private double CalculateTaxAmount() =>
            Math.Round(CalculateTaxBaseAmount() * TaxRate / 100, 2);

        private double CalculateDiscountAmount() =>
            Math.Round(CalculateTaxBaseAmount() * DiscountRate / 100, 2);

        private double CalculateTaxBaseAmount() =>
            Math.Round(Quantity * Price, 2);

        private double CalculateTotalAmount() =>
            Math.Round(CalculateTaxBaseAmount() + CalculateTaxAmount() - CalculateDiscountAmount(), 2);

        public override string ToString()
        {
            return $"Id: {Id}, " +
                   $"InvoiceId: {InvoiceId}, " +
                   $"LineNumber: {LineNumber}, " +
                   $"ProductCode: {ProductCode}, " +
                   $"ProductName: {ProductName}, " +
                   $"ProductDescription: {ProductDescription}, " +
                   $"Quantity: {Quantity}, " +
                   $"Price: {Price}, " +
                   $"TaxName: {TaxName}, " +
                   $"TaxRate: {TaxRate}, " +
                   $"DiscountRate: {DiscountRate}, " +
                   $"TaxAmount: {TaxAmount}, " +
                   $"DiscountAmount: {DiscountAmount}, " +
                   $"TaxBaseAmount: {TaxBaseAmount}, " +
                   $"TotalAmount: {TotalAmount}, ";
        }

    }
}