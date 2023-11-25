using SimplexInvoice.Domain.Base;

namespace SimplexInvoice.Application.Common.Interfaces.Persistance
{
    public interface IRepositoryCacheManager<T> 
        where T : Entity
    {
        bool TryGetCache<Ty>(string cacheKey, out Ty value);
        bool TryRemoveCache(string cacheKey);
        bool TrySetCache<Ty>(string cacheKey, Ty value);
    }

}