using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimplexInvoice.Domain.Users;

namespace SimplexInvoice.Infra.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(c => c.Id)
                   .IsUnique();

            builder.OwnsOne(c => c.EmailAddress, navbuilder =>
            {
                navbuilder.HasIndex(o => o.Address)
                          .IsUnique();

                navbuilder.Property(o => o.Address)
                          .HasColumnName("EmailAddress")
                          .HasMaxLength(40)
                          .IsRequired();
            });

            builder.Property(c => c.Password)
                   .IsRequired();

            builder.Property(c => c.FirstName)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(c => c.LastName)
                   .HasMaxLength(20)
                   .IsRequired();
        }

    }
}
