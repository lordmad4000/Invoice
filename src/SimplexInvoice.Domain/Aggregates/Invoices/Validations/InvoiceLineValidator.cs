using FluentValidation;

namespace SimplexInvoice.Domain.Invoices.Validations
{
    public class InvoiceLineValidator : AbstractValidator<InvoiceLine>
    {
        public void ValidateLineNumber()
        {
            RuleFor(c => c.LineNumber).NotNull()
                                      .GreaterThanOrEqualTo(1);
        }

        public void ValidateProductCode()
        {
            RuleFor(c => c.ProductCode).NotEmpty()
                                       .NotNull()
                                       .Length(1, 20);
        }

        public void ValidateProductName()
        {
            RuleFor(c => c.ProductName).NotEmpty()
                                       .NotNull()
                                       .Length(1, 40);
        }

        public void ValidateProductDescription()
        {
            RuleFor(c => c.ProductDescription).NotEmpty()
                                              .NotNull()
                                              .Length(1, 40);
        }

        public void ValidatePrice()
        {
            RuleFor(c => c.Price.Amount).NotNull()
                                        .GreaterThanOrEqualTo(0);
        }

        public void ValidateTaxName()
        {
            RuleFor(c => c.TaxName).NotEmpty()
                                   .NotNull()
                                   .Length(1, 20);
        }

        public void ValidateTaxRate()
        {
            RuleFor(c => c.TaxRate).NotNull()
                                   .GreaterThanOrEqualTo(0)
                                   .LessThanOrEqualTo(100);
        }

        public void ValidateDiscountRate()
        {
            RuleFor(c => c.DiscountRate).NotNull()
                                        .GreaterThanOrEqualTo(0)
                                        .LessThanOrEqualTo(100);
        }

    }
}