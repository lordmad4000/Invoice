using Invoice.Domain.IdDocumentTypes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

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
