using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimplexInvoice.Domain.Entities;

namespace SimplexInvoice.Infra.Mappings
{
    public class AppConfigurationMap : IEntityTypeConfiguration<AppConfiguration>
    {
        public void Configure(EntityTypeBuilder<AppConfiguration> builder)
        {
            builder.HasIndex(c => c.Id)
                   .IsUnique();

            builder.Property(c => c.LastInvoiceNumber)
                   .IsRequired();
        }

    }
}
