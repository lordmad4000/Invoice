using SimplexInvoice.Domain.IdDocumentTypes;
using SimplexInvoice.Domain.Users;
using SimplexInvoice.Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace SimplexInvoice.Infra.Data
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<IdDocumentType> IdDocumentType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new IdDocumentTypeMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}