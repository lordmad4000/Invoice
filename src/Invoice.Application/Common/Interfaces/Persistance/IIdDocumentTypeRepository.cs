using Invoice.Domain.IdDocumentTypes;

namespace Invoice.Application.Common.Interfaces.Persistance
{
    public interface IIdDocumentTypeRepository : IAsyncRepository<IdDocumentType>
    {
    }
}