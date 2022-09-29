using Invoice.Application.Common.Interfaces.Persistance;
using Invoice.Domain.Entities;
using Invoice.Infra.Data;

namespace Invoice.Infra.Repositories
{
    public class IdDocumentTypeRepository : RepositoryBase<IdDocumentType>, IIdDocumentTypeRepository
    {
        public IdDocumentTypeRepository(EFContext dbContext, ICacheService cacheService) : base(dbContext, cacheService)
        {
        }
    }
}