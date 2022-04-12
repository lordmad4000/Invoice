using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Application.Services;

namespace Users.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetById(Guid id);
        Task<IEnumerable<UserViewModel>> GetUsers();
        Task PutUser(UserViewModel userVM);
        Task<UserViewModel> PostUser(UserViewModel userVM);
        Task<bool> DeleteUser(Guid id);
        Task<bool> ActivateUser(string activationCode);
    }
}