using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimplexInvoice.Domain.Products;
using SimplexInvoice.Domain.TaxRates;

namespace SimplexInvoice.Infra.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasIndex(c => c.Id)
                   .IsUnique();

            builder.HasIndex(c => c.Code)
                   .IsUnique();

            builder.Property(c => c.Code)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.HasIndex(c => c.Name)
                   .IsUnique();

            builder.Property(c => c.Name)
                   .HasMaxLength(40)
                   .IsRequired();

            builder.Property(c => c.Description)
                   .HasMaxLength(40)
                   .IsRequired();

            builder.Property(c => c.PackageQuantity)
                   .IsRequired()
                   .HasDefaultValue(0);

            builder.OwnsOne(c => c.UnitPrice, navbuilder =>
            {
                navbuilder.Property(o => o.Amount)
                          .HasColumnName("UnitPrice")
                          .HasDefaultValue(0)
                          .IsRequired();

                navbuilder.Property(o => o.Currency)
                          .HasColumnName("Currency")
                          .HasMaxLength(3)
                          .HasDefaultValue("EUR")
                          .IsRequired();
            });

            builder.HasOne(c => c.TaxRate)
                   .WithMany()
                   .HasForeignKey(c => c.TaxRateId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
