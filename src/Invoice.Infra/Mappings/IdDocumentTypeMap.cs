using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Invoice.Domain.Entities;

namespace Invoice.Infra.Mappings
{
    public class IdDocumentTypeMap : IEntityTypeConfiguration<IdDocumentType>
    {
        public void Configure(EntityTypeBuilder<IdDocumentType> builder)
        {
            builder.HasIndex(c => c.Id)
                   .IsUnique();
        }

    }
}
