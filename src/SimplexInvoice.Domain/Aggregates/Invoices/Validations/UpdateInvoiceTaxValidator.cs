namespace SimplexInvoice.Domain.Invoices.Validations
{
    public class UpdateInvoiceTaxValidator : InvoiceTaxValidator
    {
        public UpdateInvoiceTaxValidator()
        {
            ValidateName();
            ValidateTotal();
        }

    }
}