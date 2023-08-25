using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimplexInvoice.Domain.Invoices;

namespace SimplexInvoice.Infra.Mappings;

public class InvoiceMap : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.HasIndex(c => c.Id)
               .IsUnique();

        builder.Property(c => c.Number)
               .IsRequired();

        builder.Property(c => c.Description)
               .HasDefaultValue("")
               .HasMaxLength(40);

        builder.Property(c => c.CompanyName)
               .HasMaxLength(40)
               .IsRequired();

        builder.Property(c => c.CompanyIdDocumentType)
               .IsRequired();

        builder.Property(c => c.CompanyDocumentNumber)
               .HasMaxLength(40)
               .IsRequired();

        builder.OwnsOne(c => c.CompanyAddress, navbuilder =>
        {
            navbuilder.Property(o => o.Street)
                      .HasColumnName("CompanyStreet")
                      .HasMaxLength(40)
                      .IsRequired();

            navbuilder.Property(o => o.City)
                      .HasColumnName("CompanyCity")
                      .HasMaxLength(40)
                      .IsRequired();

            navbuilder.Property(o => o.State)
                      .HasColumnName("CompanyState")
                      .HasMaxLength(40)
                      .IsRequired();

            navbuilder.Property(o => o.Country)
                      .HasColumnName("CompanyCountry")
                      .HasMaxLength(40)
                      .IsRequired();

            navbuilder.Property(o => o.PostalCode)
                      .HasColumnName("CompanyPostalCode")
                      .HasMaxLength(40)
                      .IsRequired();
        });

        builder.OwnsOne(c => c.CompanyPhoneNumber, navbuilder =>
        {
            navbuilder.Property(o => o.Phone)
                      .HasColumnName("CompanyPhone")
                      .HasMaxLength(40)
                      .IsRequired();
        });

        builder.OwnsOne(c => c.CompanyEmailAddress, navbuilder =>
        {
            navbuilder.Property(o => o.Address)
                      .HasColumnName("CompanyEmailAddress")
                      .HasMaxLength(40)
                      .IsRequired();
        });

        builder.Property(c => c.CustomerFullName)
               .HasMaxLength(81)
               .IsRequired();

        builder.Property(c => c.CustomerIdDocumentType)
               .IsRequired();

        builder.Property(c => c.CustomerDocumentNumber)
               .HasMaxLength(40)
               .IsRequired();

        builder.OwnsOne(c => c.CustomerAddress, navbuilder =>
        {
            navbuilder.Property(o => o.Street)
                      .HasColumnName("CustomerStreet")
                      .HasMaxLength(40)
                      .IsRequired();

            navbuilder.Property(o => o.City)
                      .HasColumnName("CustomerCity")
                      .HasMaxLength(40)
                      .IsRequired();

            navbuilder.Property(o => o.State)
                      .HasColumnName("CustomerState")
                      .HasMaxLength(40)
                      .IsRequired();

            navbuilder.Property(o => o.Country)
                      .HasColumnName("CustomerCountry")
                      .HasMaxLength(40)
                      .IsRequired();

            navbuilder.Property(o => o.PostalCode)
                      .HasColumnName("CustomerPostalCode")
                      .HasMaxLength(40)
                      .IsRequired();
        });

        builder.OwnsOne(c => c.CustomerPhoneNumber, navbuilder =>
        {
            navbuilder.Property(o => o.Phone)
                      .HasColumnName("CustomerPhone")
                      .HasMaxLength(40)
                      .IsRequired();
        });

        builder.OwnsOne(c => c.CustomerEmailAddress, navbuilder =>
        {
            navbuilder.Property(o => o.Address)
                      .HasColumnName("CustomerEmailAddress")
                      .HasMaxLength(40)
                      .IsRequired();
        });

        builder.Ignore(c => c.TotalTax);

        builder.OwnsOne(c => c.TotalTax, navbuilder =>
        {
            navbuilder.Ignore(o => o.Amount);
            navbuilder.Ignore(o => o.Currency);
        });

        builder.OwnsOne(c => c.TotalDiscount, navbuilder =>
        {
            navbuilder.Ignore(o => o.Amount);
            navbuilder.Ignore(o => o.Currency);
        });

        builder.OwnsOne(c => c.TotalTaxBase, navbuilder =>
        {
            navbuilder.Ignore(o => o.Amount);
            navbuilder.Ignore(o => o.Currency);
        });

        builder.OwnsOne(c => c.Total, navbuilder =>
        {
            navbuilder.Ignore(o => o.Amount);
            navbuilder.Ignore(o => o.Currency);
        });

        //builder.Ignore(c => c.InvoiceLines);
        builder.Ignore(c => c.TotalTaxes);
        builder.Ignore(c => c.TotalTax);
        builder.Ignore(c => c.TotalDiscount);
        builder.Ignore(c => c.TotalTaxBase);
        builder.Ignore(c => c.Total);

        builder.HasMany(c => c.InvoiceLines)
               .WithOne()
               .HasForeignKey(o => o.InvoiceId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
    }

}