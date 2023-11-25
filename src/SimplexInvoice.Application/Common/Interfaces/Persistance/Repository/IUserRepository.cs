using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SimplexInvoice.Domain.Users;

namespace SimplexInvoice.Application.Common.Interfaces.Persistance
{
    public interface IUserRepository : ICacheableRepository<User>
    {
        Task<IEnumerable<User>> GetLastUsers(int take, CancellationToken cancellationToken);
    }
}