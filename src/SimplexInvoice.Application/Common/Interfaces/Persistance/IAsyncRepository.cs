using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SimplexInvoice.Domain.Base;

namespace SimplexInvoice.Application.Common.Interfaces.Persistance
{
    public interface IAsyncRepository<T> where T : Entity
    {
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> DeleteAsync(T entity);
        Task<T> GetAsync(Expression<Func<T, bool>> expression, bool tracking, string expressionCacheKey = "");
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> expression);
        bool TryGetCache<Ty>(string cacheKey, out Ty value);
        bool TryRemoveCache(string cacheKey);
        bool TrySetCache<Ty>(string cacheKey, Ty value);
        Task<int> SaveChangesAsync();
    }

}