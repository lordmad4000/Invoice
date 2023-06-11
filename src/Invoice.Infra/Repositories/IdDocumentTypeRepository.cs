using Invoice.Application.Common.Interfaces.Persistance;
using Invoice.Domain.IdDocumentTypes;
using Invoice.Infra.Data;

namespace Invoice.Infra.Repositories
{
    public class IdDocumentTypeRepository : RepositoryBase<IdDocumentType>, IIdDocumentTypeRepository
    {
        private readonly EFContext _context;
        public IdDocumentTypeRepository(EFContext dbContext, ICacheService cacheService) : base(dbContext, cacheService)
        {
            _context = dbContext;
        }

    }
}