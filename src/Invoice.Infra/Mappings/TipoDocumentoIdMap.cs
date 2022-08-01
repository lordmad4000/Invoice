using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Invoice.Domain.Entities;

namespace Invoice.Infra.Mappings
{
    public class TipoDocumentoIdMap : IEntityTypeConfiguration<TipoDocumentoId>
    {
        public void Configure(EntityTypeBuilder<TipoDocumentoId> builder)
        {
            builder.HasIndex(c => c.Id)
                   .IsUnique();
        }

    }
}
