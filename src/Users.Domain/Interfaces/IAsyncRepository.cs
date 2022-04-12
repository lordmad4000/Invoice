using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Users.Domain.Base;

namespace Users.Domain.Interfaces
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> DeleteAsync(T entity);
        Task<T> GetAsync(Expression<Func<T, bool>> expression, bool tracking = true);
        Task<List<T>> ListAsync(Expression<Func<T, bool>> expression);
    }
}