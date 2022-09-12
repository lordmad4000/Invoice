using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Invoice.Application.Common.Dto;

namespace Invoice.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetById(Guid id, bool tracking = true);
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto> Register(UserDto userDto);
        Task Update(UserDto userDto);
        Task<UserDto> PatchUpdate(UserDto userDto);
        Task<bool> Delete(Guid id);
    }
}