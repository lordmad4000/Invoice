using FluentValidation.Results;
using SimplexInvoice.Domain.Base;
using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.Invoices.Validations;
using SimplexInvoice.Domain.ValueObjects;
using System.Linq;
using System;

namespace SimplexInvoice.Domain.Invoices
{
    public partial class InvoiceTax : Aggregate
    {
        private InvoiceTax(Guid id) : base(id) { }

        public static InvoiceTax Create(string name, double total, string currency)
        {
            var invoiceTax = new InvoiceTax(Guid.NewGuid());
            invoiceTax.Update(name, total, currency);

            return invoiceTax;
        }

        public void Update(string name, double total, string currency)
        {
            Name = name;
            Total = new Money(currency, total);

            ValidationResult validator = new UpdateInvoiceTaxValidator().Validate(this);
            if (!validator.IsValid)
                throw new BusinessRuleValidationException(
                    string.Join(", ", validator.Errors.Select(x => x.ErrorMessage).ToArray()));
        }

        public override string ToString()
        {
            return $"Id: {Id}, " +
                   $"Name: {Name}" +
                   $"Total: {Total.Amount}" +
                   $"Currency: {Total.Currency}";
        }

    }
}