using Microsoft.EntityFrameworkCore;
using SimplexInvoice.Domain.IdDocumentTypes;
using SimplexInvoice.Domain.Users;
using SimplexInvoice.Domain.TaxRates;
using SimplexInvoice.Infra.Mappings;
using SimplexInvoice.Domain.Products;

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
        public DbSet<Product> Product{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new IdDocumentTypeMap());
            modelBuilder.ApplyConfiguration(new TaxRateMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}