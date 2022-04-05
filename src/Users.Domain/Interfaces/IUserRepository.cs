using Users.Domain.Entities;

namespace Users.Domain.Interfaces
{
    public interface IUserRepository : IAsyncRepository<User>
    {
    }
}