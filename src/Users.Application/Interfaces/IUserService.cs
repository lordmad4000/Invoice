using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Application.Services;

namespace Users.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> DeleteUser(Guid id);
        Task<UserViewModel> GetById(Guid id);
        Task<IEnumerable<UserViewModel>> GetUsers();
        Task<UserViewModel> PostUser(UserViewModel userVM);
        Task PutUser(UserViewModel userVM);
    }
}