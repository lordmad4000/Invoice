using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersWebAPI.Data.Entity;

namespace UsersWebAPI.Contracts
{
    public interface IUsersRepository
    {
        Task<List<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<int> PutUser(int id, User user);
        Task PostUser(User user);
        Task<User> DeleteUser(int id);
    }
}
