using Microsoft.EntityFrameworkCore;
using SimplexInvoice.Application.Common.Exceptions;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Domain.Base;
using SimplexInvoice.Infra.Data;
using SimplexInvoice.Infra.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Infra.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : Entity
    {
        private readonly DbSet<T> _dbSet;
        private readonly EFContext _context;
        public DbSet<T> DbSet => _dbSet;
        public EFContext Context => _context;

        public BaseRepository(EFContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            try
            {
                await _dbSet.AddAsync(entity, cancellationToken);

                return entity;
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.InnerException.Message);
            }
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken, bool tracking, string[] includes = null)
        {
            try
            {
                var query = _dbSet.AsQueryable();
                if (!tracking)
                    query = query.AsNoTracking();

                if (includes != null)
                    query = includes.Aggregate(query, (current, include) => current.Include(include));

                var result = await query.FirstOrDefaultAsync(expression, cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.InnerException.Message);
            }
        }

        public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken, bool tracking = false, string[] includes = null)
        {
            try
            {
                var query = _dbSet.AsQueryable();
                if (!tracking)
                    query = query.AsNoTracking();

                if (includes != null)
                    query = includes.Aggregate(query, (current, include) => current.Include(include));

                var results = await query.Where(expression)
                                         .ToListAsync(cancellationToken);

                return results;
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.InnerException.Message);
            }
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                throw new OperationCanceledException("Update operation cancelled.");

            _dbSet.Update(entity);

            return await Task.FromResult(entity);
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
        }

    }
}