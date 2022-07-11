using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Invoice.Infra.Interfaces;
using Invoice.Domain.Base;
using Invoice.Infra.Data;
using Invoice.Infra.Exceptions;
using Invoice.Application.Common.Interfaces.Persistance;

namespace Invoice.Infra.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : BaseEntity
    {
        private readonly EFContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly string _cacheKey = $"{typeof(T)}";
        private readonly ICacheService _cacheService;

        public RepositoryBase(EFContext context, ICacheService cacheService = null)
        {
            _context = context;
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
            T result;

            var cacheKey = String.IsNullOrEmpty(expressionCacheKey) ? "" : $"{_cacheKey}{expressionCacheKey}";

            if (!TryGetCache(cacheKey, out result))
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
            List<T> result;

            if (!TryGetCache(_cacheKey, out result))
            {
                try
                {
                    result = await _dbSet.Where(expression).ToListAsync();
                }
                catch (Exception ex)
                {
                    throw new DataBaseException(ex.InnerException.Message);
                }

                TrySetCache(_cacheKey, result);
            }

            return result;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);

            TryRemoveCache(_cacheKey);

            return await Task.FromResult(entity);
        }

        public bool TryGetCache<Ty>(string cacheKey, out Ty value)
        {
            value = default(Ty);
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
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
            }

            return false;
        }

    }
}