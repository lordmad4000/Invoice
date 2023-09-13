namespace SimplexInvoice.Domain.Invoices.Validations
{
    public class UpdateInvoiceLineValidator : InvoiceLineValidator
    {
        public UpdateInvoiceLineValidator()
        {
            ValidateLineNumber();
            ValidateProductCode();
            ValidateProductName();
            ValidateProductDescription();
            ValidatePrice();
            ValidateTaxName();
            ValidateTaxRate();
            ValidateDiscountRate();
        }

    }
}