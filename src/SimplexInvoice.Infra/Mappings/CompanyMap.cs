// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using SimplexInvoice.Domain.Entities;

// namespace SimplexInvoice.Infra.Mappings
// {
//     public class CompanyMap : IEntityTypeConfiguration<Company>
//     {
//         public void Configure(EntityTypeBuilder<Company> builder)
//         {
//             builder.HasIndex(c => c.Id)
//                    .IsUnique();

//             builder.OwnsOne(c => c.EmailAddress)
//                    .Property(c => c.Address)
//                    .HasColumnName("EmailAddress")
//                    .IsRequired();
//         }

//     }
// }
