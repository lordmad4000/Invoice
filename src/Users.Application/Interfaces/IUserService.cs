using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Application.Models;

namespace Users.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserById(Guid id, bool tracking = true);
        Task<IEnumerable<UserDto>> GetUsers();
        Task<UserDto> RegisterUser(UserDto userDto);
        Task UpdateUser(UserDto userDto);
        Task<UserDto> PatchUser(UserDto userDto);
        Task<bool> DeleteUser(Guid id);
        Task<bool> ActivateUser(string activationCode);
        Task<UserDto> Login(string username, string password);
    }
}