using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimplexInvoice.Domain.Customers;

namespace SimplexInvoice.Infra.Mappings;

public class CustomerMap : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasIndex(c => c.Id)
               .IsUnique();

        builder.Property(c => c.FirstName)
               .HasMaxLength(40)
               .IsRequired();

        builder.Property(c => c.LastName)
               .HasMaxLength(40)
               .IsRequired();

        builder.HasIndex(p => new { p.IdDocumentTypeId, p.IdDocumentNumber })
               .IsUnique();

        builder.Property(c => c.IdDocumentNumber)
               .HasMaxLength(40)
               .IsRequired();

        builder.OwnsOne(c => c.Address, navbuilder =>
        {
            navbuilder.Property(o => o.Street)
                      .HasColumnName("Street")
                      .HasMaxLength(40)
                      .IsRequired();

            navbuilder.Property(o => o.City)
                      .HasColumnName("City")
                      .HasMaxLength(40)
                      .IsRequired();

            navbuilder.Property(o => o.State)
                      .HasColumnName("State")
                      .HasMaxLength(40)
                      .IsRequired();

            navbuilder.Property(o => o.Country)
                      .HasColumnName("Country")
                      .HasMaxLength(40)
                      .IsRequired();

            navbuilder.Property(o => o.PostalCode)
                      .HasColumnName("PostalCode")
                      .HasMaxLength(40)
                      .IsRequired();
        });

        builder.OwnsOne(c => c.Phone, navbuilder =>
        {
            navbuilder.Property(o => o.Phone)
                      .HasColumnName("Phone")
                      .HasMaxLength(40)
                      .IsRequired();
        });

        builder.OwnsOne(c => c.EmailAddress, navbuilder =>
        {
            navbuilder.HasIndex(o => o.Address)
                      .IsUnique();

            navbuilder.Property(o => o.Address)
                      .HasColumnName("EmailAddress")
                      .HasMaxLength(40)
                      .IsRequired();
        });

        builder.HasOne(c => c.IdDocumentType)
               .WithMany()
               .HasForeignKey(o => o.IdDocumentTypeId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

    }

}