using System.Collections.Generic;
using System.Threading.Tasks;
using SimplexInvoice.Domain.Users;

namespace SimplexInvoice.Application.Common.Interfaces.Persistance
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<List<User>> GetLastUsers(int take);
    }
}