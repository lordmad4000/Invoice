using Microsoft.EntityFrameworkCore;
using SimplexInvoice.Domain.IdDocumentTypes;
using SimplexInvoice.Domain.Users;
using SimplexInvoice.Domain.TaxRates;
using SimplexInvoice.Infra.Mappings;
using SimplexInvoice.Domain.Products;
using SimplexInvoice.Domain.Companies;
using SimplexInvoice.Domain.Customers;
using SimplexInvoice.Domain.Invoices;

namespace SimplexInvoice.Infra.Data
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<IdDocumentType> IdDocumentType { get; set; }
        public DbSet<TaxRate> TaxRate { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Invoice> Invoice{ get; set; }
        public DbSet<InvoiceLine> InvoiceLine { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new IdDocumentTypeMap());
            modelBuilder.ApplyConfiguration(new TaxRateMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new CompanyMap());
            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new InvoiceMap());
            modelBuilder.ApplyConfiguration(new InvoiceLineMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}