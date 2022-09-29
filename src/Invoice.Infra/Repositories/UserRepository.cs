using Invoice.Application.Common.Interfaces.Persistance;
using Invoice.Domain.Entities;
using Invoice.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.Infra.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly EFContext _context;
        public UserRepository(EFContext dbContext, ICacheService cacheService) : base(dbContext, cacheService)
        {
            _context = dbContext;
        }

        public async Task<List<User>> GetLastUsers(int take)
        {
            var query = _context.User.AsNoTracking();

            return await query.OrderByDescending(x => x.CreationDate)
                              .Take(take)
                              .ToListAsync();
        }
    }
}