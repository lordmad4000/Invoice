using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Invoice.Domain.Entities;

namespace Invoice.Infra.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(c => c.Id)
                   .IsUnique();

            //builder.OwnsOne(c => c.EmailAddress)
            //       .Property(c => c.Address)
            //       .HasColumnName("EmailAddress")
            //       .IsRequired();

            builder.OwnsOne(c => c.EmailAddress, navbuilder =>
            {
                navbuilder.HasIndex(o => o.Address)
                          .IsUnique();
                navbuilder.Property(o => o.Address)
                          .HasColumnName("EmailAddress")
                          .IsRequired();
            });
        }

    }
}
