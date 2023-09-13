using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Domain.IdDocumentTypes;
using SimplexInvoice.Infra.Data;

namespace SimplexInvoice.Infra.Repositories
{
    public class IdDocumentTypeRepository : RepositoryBase<IdDocumentType>, IIdDocumentTypeRepository
    {
        private readonly EFContext _context;
        public IdDocumentTypeRepository(IUnitOfWork unitOfWork, ICacheService cacheService) : base(unitOfWork, cacheService)
        {
            _context = unitOfWork.GetContext();
        }

    }
}