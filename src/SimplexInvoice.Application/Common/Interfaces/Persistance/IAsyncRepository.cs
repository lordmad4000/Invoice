using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using SimplexInvoice.Domain.Base;

namespace SimplexInvoice.Application.Common.Interfaces.Persistance
{
    public interface IAsyncRepository<T> where T : Entity
    {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        void Delete(T entity, CancellationToken cancellationToken);
        Task<T> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken, bool tracking, string expressionCacheKey = "", string[] includes = null);
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken, bool tracking = false, string[] includes = null);
        bool TryGetCache<Ty>(string cacheKey, out Ty value);
        bool TryRemoveCache(string cacheKey);
        bool TrySetCache<Ty>(string cacheKey, Ty value);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }

}