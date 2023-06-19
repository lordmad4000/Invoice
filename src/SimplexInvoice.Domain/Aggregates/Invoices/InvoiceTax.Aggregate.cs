using FluentValidation.Results;
using SimplexInvoice.Domain.Base;
using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.Invoices.Validations;
using System.Linq;
using System;

namespace SimplexInvoice.Domain.Invoices
{
    public partial class InvoiceTax : Aggregate
    {
        private InvoiceTax(Guid id) : base(id) { }

        public static InvoiceTax Create(string name, double total)
        {
            var invoiceTax = new InvoiceTax(Guid.NewGuid());
            invoiceTax.Update(name, total);

            return invoiceTax;
        }

        public void Update(string name, double total)
        {
            Name = name;
            Total = total;

            ValidationResult validator = new UpdateInvoiceTaxValidator().Validate(this);
            if (!validator.IsValid)
                throw new BusinessRuleValidationException(
                    string.Join(", ", validator.Errors.Select(x => x.ErrorMessage).ToArray()));
        }

        public override string ToString()
        {
            return $"Id: {Id}, " +
                   $"Name: {Name}" +
                   $"Total: {Total}";
        }

    }
}