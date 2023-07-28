using FluentValidation;
using SimplexInvoice.Domain.Exceptions;

namespace SimplexInvoice.Domain.Invoices.Validations
{
    public class UpdateInvoiceValidator : InvoiceValidator
    {
        public UpdateInvoiceValidator()
        {
            ValidateNumber();;
            ValidateDescription();;
            ValidateCompanyName(); ;
            ValidateCompanyIdDocumentType(); ;
            ValidateCompanyIdDocumentNumber();
            ValidateCompanyEmailAddress();
            ValidateCustomerFullName();;
            ValidateCustomerIdDocumentType();;
            ValidateCustomerIdDocumentNumber();
            ValidateCustomerEmailAddress();
            ValidateInvoiceLines();
        }

    }
}