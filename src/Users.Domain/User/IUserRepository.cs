using Users.Domain.Interfaces;

namespace Users.Domain.User
{
    public interface IUserRepository : IAsyncRepository<User>
    {
    }
}