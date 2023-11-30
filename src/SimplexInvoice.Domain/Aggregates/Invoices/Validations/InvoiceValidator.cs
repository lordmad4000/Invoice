using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using FluentValidation;

namespace SimplexInvoice.Domain.Invoices.Validations
{
    public class InvoiceValidator : AbstractValidator<Invoice>
    {
        public void ValidateNumber()
        {
            RuleFor(c => c.Number).NotEmpty()
                                  .MaximumLength(20);
        }

        public void ValidateCorrectionNumber()
        {
            RuleFor(c => c.CorrectionNumber).MaximumLength(20);
        }

        public void ValidateDescription()
        {
            RuleFor(c => c.Description).NotNull()
                                       .MaximumLength(40);
        }

        public void ValidateCompanyName()
        {
            RuleFor(c => c.CompanyName).NotEmpty()
                                       .Length(1, 40);
        }

        public void ValidateCompanyIdDocumentType()
        {
            RuleFor(c => c.CompanyIdDocumentType).NotEmpty();
        }

        public void ValidateCompanyIdDocumentNumber()
        {
            RuleFor(c => c.CompanyDocumentNumber).NotEmpty()
                                                  .Length(1, 40);
        }

        public void ValidateCompanyEmailAddress()
        {
            RuleFor(c => c.CompanyEmailAddress.Address).NotEmpty()                                                
                                                       .Length(1, 40);
        }

        public void ValidateCustomerFullName()
        {
            RuleFor(c => c.CustomerFullName).NotEmpty()
                                            .Length(1, 81);
        }

        public void ValidateCustomerIdDocumentType()
        {
            RuleFor(c => c.CustomerIdDocumentType).NotEmpty();
        }

        public void ValidateCustomerIdDocumentNumber()
        {
            RuleFor(c => c.CustomerDocumentNumber).NotEmpty()
                                                  .Length(1, 40);
        }

        public void ValidateCustomerEmailAddress()
        {
            RuleFor(c => c.CustomerEmailAddress.Address).NotEmpty()
                                                       .Length(1, 40);
        }

        public void ValidateInvoiceLines()
        {
            RuleFor(c => c.InvoiceLines).Must(x => x.Count > 0)
                                        .WithMessage("InvoiceLines must be greater than 0");
        }

    }
}