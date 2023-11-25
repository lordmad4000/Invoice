using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Domain.Users;
using SimplexInvoice.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace SimplexInvoice.Infra.Repositories
{
    public class UserRepository : CachedRepositoryDecorator<User>, IUserRepository
    {
        public UserRepository(EFContext context, 
                              ICacheService cacheService, 
                              IRepository<User> repository,
                              ICustomLogger logger)
            : base(context, cacheService, repository, logger)
        {
        }

        public async Task<IEnumerable<User>> GetLastUsers(int take, CancellationToken cancellationToken)
        {
            var query = _context.User.AsNoTracking();

            return await query.OrderByDescending(x => x.CreationDate)
                              .Take(take)
                              .ToListAsync(cancellationToken);
        }
    }
}