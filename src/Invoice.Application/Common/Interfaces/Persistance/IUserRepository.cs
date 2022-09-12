using System.Collections.Generic;
using System.Threading.Tasks;
using Invoice.Domain.Entities;

namespace Invoice.Application.Common.Interfaces.Persistance
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<List<User>> GetLastUsers(int take);
    }
}