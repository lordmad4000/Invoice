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
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly EFContext _context;
        public UserRepository(IUnitOfWork unitOfWork, ICacheService cacheService) : base(unitOfWork, cacheService)
        {
            _context = unitOfWork.GetContext();
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