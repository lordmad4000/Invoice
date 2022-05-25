using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Application.Models;

namespace Users.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetById(Guid id, bool tracking = true);
        Task<IEnumerable<UserDto>> GetUsers();
        Task PutUser(UserDto userDto);
        Task<UserDto> PostUser(UserDto userDto);
        Task<bool> DeleteUser(Guid id);
        Task<bool> ActivateUser(string activationCode);
        Task<UserDto> Login(string username, string password);
        string GetToken(string userId, string userEmail, string secretKey);
        Task<UserDto> PatchUser(UserDto userDto);
    }
}