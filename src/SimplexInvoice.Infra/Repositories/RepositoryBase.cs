using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Domain.Base;
using SimplexInvoice.Infra.Data;
using SimplexInvoice.Infra.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Threading;
using SimplexInvoice.Domain.Exceptions;

namespace SimplexInvoice.Infra.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : Entity
    {
        private readonly DbSet<T> _dbSet;
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _cacheKey = $"{typeof(T)}";
        private readonly ICacheService _cacheService;
        public RepositoryBase(IUnitOfWork unitOfWork, ICacheService cacheService = null)
        {
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.GetContext().Set<T>();
            _cacheService = cacheService;
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            TryRemoveCache(_cacheKey);

            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity is null)
                throw new NotFoundException($"{typeof(T).Name} not found.");

            Delete(entity, cancellationToken);
        }

        public void Delete(T entity, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                throw new OperationCanceledException("Delete operation cancelled.");

            _dbSet.Remove(entity);
            TryRemoveCache(_cacheKey);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken, bool tracking, string expressionCacheKey = "")
        {
            var cacheKey = string.IsNullOrEmpty(expressionCacheKey) ? "" : $"{_cacheKey}{expressionCacheKey}";

            if (!TryGetCache(cacheKey, out T result))
            {
                try
                {
                    if (tracking)
                        result = await _dbSet.FirstOrDefaultAsync(expression, cancellationToken);

                    else
                        result = await _dbSet.AsNoTracking()
                                             .FirstOrDefaultAsync(expression, cancellationToken);
                }
                catch (Exception ex)
                {
                    throw new DataBaseException(ex.InnerException.Message);
                }
                if (!String.IsNullOrEmpty(cacheKey))
                    TrySetCache(cacheKey, result);
            }

            return result;
        }

        public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
        {
            if (!TryGetCache(_cacheKey, out List<T> results))
            {
                try
                {
                    results = await _dbSet.Where(expression)
                                          .ToListAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    throw new DataBaseException(ex.InnerException.Message);
                }
                TrySetCache(_cacheKey, results);
            }

            return results;
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                throw new OperationCanceledException("Update operation cancelled.");

            _dbSet.Update(entity);
            TryRemoveCache(_cacheKey);

            return await Task.FromResult(entity);
        }

        public bool TryGetCache<Ty>(string cacheKey, out Ty value)
        {
            value = default;
            try
            {
                if (_cacheService != null)
                {
                    if (_cacheService.TryGet(cacheKey, out value))
                        return true;
                }
            }
            catch (Exception ex)
            {
                throw new CacheException(ex.InnerException.Message);
            }

            return false;
        }

        public bool TryRemoveCache(string cacheKey)
        {
            try
            {
                if (_cacheService != null)
                {
                    _cacheService.Remove($"{cacheKey}*");
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new CacheException(ex.InnerException.Message);
            }

            return false;
        }

        public bool TrySetCache<Ty>(string cacheKey, Ty value)
        {
            try
            {
                if (_cacheService != null)
                {
                    _cacheService.Set(cacheKey, value);

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new CacheException(ex.InnerException.Message);
            }

            return false;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                throw new OperationCanceledException("Operation cancelled.");

            return await _unitOfWork.SaveChangesAsync();
        }

    }
}