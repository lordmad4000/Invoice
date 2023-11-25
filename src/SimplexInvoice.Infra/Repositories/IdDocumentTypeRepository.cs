using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Domain.IdDocumentTypes;
using SimplexInvoice.Infra.Data;

namespace SimplexInvoice.Infra.Repositories
{
    public class IdDocumentTypeRepository : CachedRepositoryDecorator<IdDocumentType>, IIdDocumentTypeRepository
    {
        public IdDocumentTypeRepository(EFContext context, 
                                        ICacheService cacheService, 
                                        IRepository<IdDocumentType> repository,
                                        ICustomLogger logger)
            : base(context, cacheService, repository, logger)
        {
        }

    }
}