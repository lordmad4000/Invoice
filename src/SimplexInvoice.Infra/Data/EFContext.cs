using Microsoft.EntityFrameworkCore;
using SimplexInvoice.Domain.Companies;
using SimplexInvoice.Domain.Customers;
using SimplexInvoice.Domain.Entities;
using SimplexInvoice.Domain.IdDocumentTypes;
using SimplexInvoice.Domain.Invoices;
using SimplexInvoice.Domain.Products;
using SimplexInvoice.Domain.TaxRates;
using SimplexInvoice.Domain.Users;
using SimplexInvoice.Infra.Mappings;

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
        public DbSet<AppConfiguration> AppConfiguration { get; set; }

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
            modelBuilder.ApplyConfiguration(new AppConfigurationMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}