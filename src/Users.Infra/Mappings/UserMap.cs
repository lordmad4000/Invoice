using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.Entities;

namespace Users.Infra.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(c => c.Id)
                   .IsUnique();

            builder.HasIndex(c => c.UserName)
                   .IsUnique();

            builder.OwnsOne(c => c.EmailAddress)
                   .Property(c => c.Address)
                   .HasColumnName("EmailAddress")
                   .IsRequired();
        }

    }
}
