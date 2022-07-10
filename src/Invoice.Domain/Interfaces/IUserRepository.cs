using Invoice.Domain.Entities;

namespace Invoice.Domain.Interfaces
{
    public interface IUserRepository : IAsyncRepository<User>
    {
    }
}