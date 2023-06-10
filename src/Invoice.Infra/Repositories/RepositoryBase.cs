using Invoice.Application.Common.Interfaces.Persistance;
using Invoice.Domain.Base;
using Invoice.Infra.Data;
using Invoice.Infra.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Invoice.Infra.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : AggregateRoot
    {
        private readonly DbSet<T> _dbSet;
        private readonly string _cacheKey = $"{typeof(T)}";
        private readonly ICacheService _cacheService;
        public RepositoryBase(EFContext context, ICacheService cacheService = null)
        {
            _dbSet = context.Set<T>();
            _cacheService = cacheService;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);

            TryRemoveCache(_cacheKey);

            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity != null)
                return await DeleteAsync(entity);

            return await Task.FromResult(false);
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);

            TryRemoveCache(_cacheKey);

            return await Task.FromResult(true);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression, bool tracking, string expressionCacheKey = "")
        {
            var cacheKey = string.IsNullOrEmpty(expressionCacheKey) ? "" : $"{_cacheKey}{expressionCacheKey}";

            if (!TryGetCache(cacheKey, out T result))
            {
                try
                {
                    if (tracking)
                        result = await _dbSet.FirstOrDefaultAsync(expression);

                    else
                        result = await _dbSet.AsNoTracking()
                                             .FirstOrDefaultAsync(expression);
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

        public async Task<List<T>> ListAsync(Expression<Func<T, bool>> expression)
        {
            if (!TryGetCache(_cacheKey, out List<T> results))
            {
                try
                {
                    results = await _dbSet.Where(expression).ToListAsync();
                }
                catch (Exception ex)
                {
                    throw new DataBaseException(ex.InnerException.Message);
                }

                TrySetCache(_cacheKey, results);
            }

            return results;
        }

        public async Task<T> UpdateAsync(T entity)
        {
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

    }
}