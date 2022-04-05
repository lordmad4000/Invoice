using Users.Domain.Interfaces;

namespace Users.Domain.Interfaces
{
    public interface IUserRepository : IAsyncRepository<User>
    {
    }
}