using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimplexInvoice.Domain.TaxRates;

namespace SimplexInvoice.Infra.Mappings
{
    public class TaxRateMap : IEntityTypeConfiguration<TaxRate>
    {
        public void Configure(EntityTypeBuilder<TaxRate> builder)
        {
            builder.HasIndex(c => c.Id)
                   .IsUnique();

            builder.HasIndex(c => c.Name)
                   .IsUnique();

            builder.Property(c => c.Name)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(c => c.Value)
                   .HasDefaultValue(0)
                   .IsRequired();
        }

    }
}
