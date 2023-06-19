using FluentValidation;

namespace SimplexInvoice.Domain.Invoices.Validations
{
    public class InvoiceTaxValidator : AbstractValidator<InvoiceTax>
    {
        public void ValidateName()
        {
            RuleFor(c => c.Name).NotEmpty()
                                .NotNull()
                                .Length(1, 20);
        }

        public void ValidateTotal()
        {
            RuleFor(c => c.Total).NotNull()
                                 .GreaterThanOrEqualTo(0);
        }

    }
}