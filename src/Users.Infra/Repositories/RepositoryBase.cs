using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Users.CrossCutting.Interfaces;
using Users.Domain.Base;
using Users.Domain.Interfaces;
using Users.Infra.Data;

namespace Users.Infra.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : BaseEntity
    {
        private readonly EFContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly string _cacheKey = $"{typeof(T)}";
        private readonly IMemoryCacheService _memoryCacheService;

        public RepositoryBase(EFContext context, IMemoryCacheService memoryCacheService = null)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _memoryCacheService = memoryCacheService;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return await Task.FromResult(true);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression, bool tracking = true)
        {
            if (tracking)
                return await _dbSet.FirstOrDefaultAsync(expression);

            return await _dbSet.AsNoTracking()
                               .FirstOrDefaultAsync(expression);
        }

        public async Task<List<T>> ListAsync(Expression<Func<T, bool>> expression)
        {
            var result = new List<T>();
            if (_memoryCacheService != null)
            {
                if (!_memoryCacheService.TryGet(_cacheKey, out result))
                {
                    result = await _dbSet.Where(expression).ToListAsync();
                    _memoryCacheService.Set(_cacheKey, result);
                }
            }
            else
                result = await _dbSet.Where(expression).ToListAsync();

            return result;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return await Task.FromResult(entity);
        }

    }
}