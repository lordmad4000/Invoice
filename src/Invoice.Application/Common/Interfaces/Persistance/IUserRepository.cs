using Invoice.Domain.Entities;

namespace Invoice.Application.Common.Interfaces.Persistance
{
    public interface IUserRepository : IAsyncRepository<User>
    {
    }
}