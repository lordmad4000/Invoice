using Invoice.Application.Common.Interfaces.Persistance;
using Invoice.Domain.Entities;
using Invoice.Infra.Data;
using Invoice.Infra.Interfaces;

namespace Invoice.Infra.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(EFContext dbContext, ICacheService cacheService) : base(dbContext, cacheService)
        {
        }
    }
}