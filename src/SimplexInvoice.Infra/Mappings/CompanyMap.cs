using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimplexInvoice.Domain.Companies;

namespace SimplexInvoice.Infra.Mappings;

public class CompanyMap : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasIndex(c => c.Id)
               .IsUnique();

        builder.HasIndex(c => c.Name)
               .IsUnique();

        builder.Property(c => c.Name)
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