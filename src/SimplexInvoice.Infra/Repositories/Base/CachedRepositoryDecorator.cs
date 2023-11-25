using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Domain.Base;
using SimplexInvoice.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Infra.Repositories
{
    public class CachedRepositoryDecorator<T> : RepositoryCacheManager<T>, ICacheableRepository<T>
        where T : Entity
    {
        protected readonly IRepository<T> _repository;
        protected readonly EFContext _context;
        public CachedRepositoryDecorator(EFContext context, 
                                         ICacheService cacheService, 
                                         IRepository<T> repository, 
                                         ICustomLogger logger)
            : base(cacheService, logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _repository = repository;
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(entity, cancellationToken);
            TryRemoveCache(_cacheKey);

            return entity;
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(entity, cancellationToken);
            TryRemoveCache(_cacheKey);

            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(id, cancellationToken);
            TryRemoveCache(_cacheKey);
        }

        public void Delete(T entity, CancellationToken cancellationToken)
        {
            _repository.Delete(entity, cancellationToken);
            TryRemoveCache(_cacheKey);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken, bool tracking, string expressionCacheKey = "", string[] includes = null)
        {
            var cacheKey = string.IsNullOrEmpty(expressionCacheKey) ? "" : $"{_cacheKey}{expressionCacheKey}";
            if (!TryGetCache(cacheKey, out T result))
            {
                result = await _repository.GetAsync(expression, cancellationToken, tracking, includes);
                if (!String.IsNullOrEmpty(cacheKey))
                    TrySetCache(cacheKey, result);
            }

            return result;
        }

        public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken, bool tracking = false, string[] includes = null)
        {
            if (!TryGetCache(_cacheKey, out IEnumerable<T> results))
            {
                results = await _repository.ListAsync(expression, cancellationToken, tracking, includes);
                if (results.Any())
                    TrySetCache(_cacheKey, results);
            }

            return results;
        }

    }
}