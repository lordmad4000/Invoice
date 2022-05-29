using Users.CrossCutting.Interfaces;
using Users.Domain.Entities;
using Users.Domain.Interfaces;
using Users.Infra.Data;

namespace Users.Infra.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(EFContext dbContext, ICacheService cacheService) : base(dbContext, cacheService)
        {
        }
    }
}