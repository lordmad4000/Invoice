using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimplexInvoice.Domain.Invoices;

namespace SimplexInvoice.Infra.Mappings;

public class InvoiceLineMap : IEntityTypeConfiguration<InvoiceLine>
{
    public void Configure(EntityTypeBuilder<InvoiceLine> builder)
    {
        builder.HasIndex(c => c.Id)
               .IsUnique();

        builder.HasIndex(p => new { p.Id, p.LineNumber })
               .IsUnique();

        builder.Property(c => c.LineNumber)
               .IsRequired();

        builder.Property(c => c.ProductCode)
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(c => c.ProductName)
               .HasMaxLength(40)
               .IsRequired();

        builder.Property(c => c.ProductDescription)
               .HasMaxLength(40);

        builder.Property(c => c.Packages)
               .HasDefaultValue(0)
               .IsRequired();

        builder.Property(c => c.Quantity)
               .HasDefaultValue(0)
               .IsRequired();

        builder.OwnsOne(c => c.Price, navbuilder =>
        {
            navbuilder.Property(o => o.Amount)
                      .HasColumnName("Price")
                      .HasDefaultValue(0)
                      .IsRequired();

            navbuilder.Property(o => o.Currency)
                      .HasColumnName("Currency")
                      .HasMaxLength(3)
                      .HasDefaultValue("EUR")
                      .IsRequired();
        });

        builder.Property(c => c.TaxName)
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(c => c.TaxRate)
               .HasDefaultValue(0)
               .IsRequired();

        builder.Property(c => c.DiscountRate)
               .HasDefaultValue(0)
               .IsRequired();

        builder.Ignore(c => c.Tax);
        builder.Ignore(c => c.Discount);
        builder.Ignore(c => c.TaxBase);
        builder.Ignore(c => c.Total);
    }

}