using SimplexInvoice.Domain.IdDocumentTypes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace SimplexInvoice.Infra.Mappings
{
    public class IdDocumentTypeMap : IEntityTypeConfiguration<IdDocumentType>
    {
        public void Configure(EntityTypeBuilder<IdDocumentType> builder)
        {
            builder.HasIndex(c => c.Id)
                   .IsUnique();

            builder.HasIndex(c => c.Name)
                   .IsUnique();

            builder.Property(c => c.Name)
                   .HasMaxLength(20)
                   .IsRequired();
        }

    }
}
