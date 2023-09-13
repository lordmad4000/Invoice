using FluentValidation.Results;
using SimplexInvoice.Domain.Base;
using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.Invoices.Validations;
using SimplexInvoice.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SimplexInvoice.Domain.Invoices
{
    public partial class Invoice : AggregateRoot
    {
        private Invoice(Guid id) : base(id) { }

        public static Invoice Create(string number,
                                     string description,
                                     string companyName,
                                     string companyIdDocumentType,
                                     string companyDocumentNumber,
                                     Address companyAddress,
                                     PhoneNumber companyPhoneNumber,
                                     EmailAddress companyEmailAddress,
                                     string customerFullName,
                                     string customerIdDocumentType,
                                     string customerDocumentNumber,
                                     Address customerAddress,
                                     PhoneNumber customerPhoneNumber,
                                     EmailAddress customerEmailAddress,
                                     ICollection<InvoiceLine> invoiceLines)
        {
            var invoice = new Invoice(Guid.NewGuid());
            invoice.Update(number,
                           description,
                           companyName,
                           companyIdDocumentType,
                           companyDocumentNumber,
                           companyAddress,
                           companyPhoneNumber,
                           companyEmailAddress,
                           customerFullName,
                           customerIdDocumentType,
                           customerDocumentNumber,
                           customerAddress,
                           customerPhoneNumber,
                           customerEmailAddress,
                           invoiceLines);

            return invoice;
        }
        public void Update(string number,
                           string description,
                           string companyName,
                           string companyIdDocumentType,
                           string companyDocumentNumber,
                           Address companyAddress,
                           PhoneNumber companyPhoneNumber,
                           EmailAddress companyEmailAddress,
                           string customerFullName,
                           string customerIdDocumentType,
                           string customerDocumentNumber,
                           Address customerAddress,
                           PhoneNumber customerPhoneNumber,
                           EmailAddress customerEmailAddress,
                           ICollection<InvoiceLine> invoiceLines)
        {
            Number = number;
            Description = description;
            CompanyName = companyName;
            CompanyIdDocumentType = companyIdDocumentType;
            CompanyDocumentNumber = companyDocumentNumber;
            CompanyAddress = companyAddress;
            CompanyPhoneNumber = companyPhoneNumber;
            CompanyEmailAddress = companyEmailAddress;
            CustomerFullName = customerFullName;
            CustomerIdDocumentType = customerIdDocumentType;
            CustomerDocumentNumber = customerDocumentNumber;
            CustomerAddress = customerAddress;
            CustomerPhoneNumber = customerPhoneNumber;
            CustomerEmailAddress = customerEmailAddress;
            _invoiceLines.Clear();
            AddLines(invoiceLines);
        }

        private void Validate()
        {
            ValidationResult validator = new UpdateInvoiceValidator().Validate(this);

            if (DistinctInvoiceLinesCurrencies())
                validator.Errors.Add(new ValidationFailure("InvoiceLines", "Distinct invoice lines currencies not allowed."));

            if (!validator.IsValid)
                throw new BusinessRuleValidationException(
                    string.Join(", ", validator.Errors.Select(x => x.ErrorMessage).ToArray()));
        }

        public void CalculateAmounts()
        {
            TryClearAndReplaceLines(InvoiceLines.ToList());
        }

        private void CalculateTotals()
        {
            SetInvoiceTaxes();
            string currency = _invoiceLines.Select(c => c.Price.Currency).First();
            TotalTax = new Money(currency, _invoiceLines.Sum(c => c.Tax.Amount));
            TotalDiscount = new Money(currency, _invoiceLines.Sum(c => c.Discount.Amount));
            TotalTaxBase = new Money(currency, _invoiceLines.Sum(c => c.TaxBase.Amount));
            Total = new Money(currency, TotalTaxBase.Amount + TotalTax.Amount - TotalDiscount.Amount);
        }

        private void SetInvoiceTaxes()
        {
            HashSet<TotalTax>? totalTaxes = _invoiceLines.GroupBy(c => c.TaxName)
                                                         .Select(g => CalculateTotalTax(g.ToList()))
                                                         .Where(c => c.Amount != 0)
                                                         .ToHashSet();

            _totalTaxes.Clear();
            _totalTaxes.UnionWith(totalTaxes);
        }

        private static TotalTax CalculateTotalTax(List<InvoiceLine> invoiceLines)
        {
            string taxName = invoiceLines.First().TaxName;
            int taxRate = invoiceLines.First().TaxRate;
            double baseAmount = invoiceLines.Sum(c => c.Quantity * c.Price.Amount);
            double amount = baseAmount * taxRate / 100;

            return new TotalTax(taxName, baseAmount, amount);
        }

        public void AddLine(InvoiceLine invoiceLine)
        {
            invoiceLine.CalculateAmounts();
            _invoiceLines.Add(invoiceLine);
            Validate();
            Reenumerate();
            CalculateTotals();
        }

        public void AddLines(ICollection<InvoiceLine> invoiceLines)
        {
            foreach (var invoiceLine in invoiceLines)
            {
                invoiceLine.CalculateAmounts();
                _invoiceLines.Add(invoiceLine);
            }

            Validate();
            Reenumerate();
            CalculateTotals();
        }

        public bool TryClearAndReplaceLines(ICollection<InvoiceLine> invoiceLines)
        {
            var oldInvoiceLines = _invoiceLines.ToList();
            _invoiceLines.Clear();
            try
            {
                AddLines(invoiceLines);
            }
            catch
            {
                _invoiceLines.Clear();
                foreach (var invoiceLine in oldInvoiceLines)
                    _invoiceLines.Add(invoiceLine);

                return false;
            }

            return true;
        }

        public bool TryRemoveLine(int lineNumber)
        {
            InvoiceLine? invoiceLine = _invoiceLines.FirstOrDefault(c => c.LineNumber.Equals(lineNumber));

            if (invoiceLine is null)
                return false;

            return TryRemoveLine(invoiceLine);
        }

        public bool TryRemoveLine(InvoiceLine invoiceLine)
        {
            if (_invoiceLines.Count <= 1)
                return false;

            if (!_invoiceLines.Remove(invoiceLine))
                return false;

            Reenumerate();
            CalculateTotals();

            return true;
        }

        private bool DistinctInvoiceLinesCurrencies() => _invoiceLines.GroupBy(c => c.Price.Currency)
                                                                      .Count() > 1;

        private void Reenumerate()
        {
            int lineNumber = 0;
            var sortedInvoiceLines = _invoiceLines.OrderBy(c => c.LineNumber);
            foreach (var invoiceLine in sortedInvoiceLines)
            {
                lineNumber++;
                invoiceLine.Update(Id,
                                   lineNumber,
                                   invoiceLine.ProductCode,
                                   invoiceLine.ProductName,
                                   invoiceLine.ProductDescription,
                                   invoiceLine.Packages,
                                   invoiceLine.Quantity,
                                   invoiceLine.Price,
                                   invoiceLine.TaxName,
                                   invoiceLine.TaxRate,
                                   invoiceLine.DiscountRate);
            }
        }

        public override string ToString()
        {
            return $"Id: {Id}, " +
                   $"Number: {Number}, " +
                   $"Description: {Description}, " +
                   $"CompanyName: {CompanyName}, " +
                   $"CompanyIdDocumentType: {CompanyIdDocumentType}, " +
                   $"CompanyDocumentNumber: {CompanyDocumentNumber}, " +
                   $"CompanyAddressStreet: {CompanyAddress.Street}, " +
                   $"CompanyAddressCity: {CompanyAddress.City}, " +
                   $"CompanyAddressCountry: {CompanyAddress.Country}, " +
                   $"CompanyAddressPostalCode: {CompanyAddress.PostalCode}, " +
                   $"CustomerFullName: {CustomerFullName}, " +
                   $"CustomerIdDocumentType: {CustomerIdDocumentType}, " +
                   $"CustomerDocumentNumber: {CustomerDocumentNumber}, " +
                   $"CustomerAddressStreet: {CustomerAddress.Street}, " +
                   $"CustomerAddressCity: {CustomerAddress.City}, " +
                   $"CustomerAddressCountry: {CustomerAddress.Country}, " +
                   $"CustomerAddressPostalCode: {CustomerAddress.PostalCode}, " +
                   $"TotalTax: {TotalTax.Amount}, " +
                   $"TotalDiscount: {TotalDiscount.Amount}, " +
                   $"TotalTaxBase: {TotalTaxBase.Amount}, " +
                   $"Total: {Total.Amount}";
        }

    }
}