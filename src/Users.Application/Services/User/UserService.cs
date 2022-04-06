using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Domain.Entities;
using Users.Domain.Interfaces;

namespace Users.Application.Services
{
    public class UserService
    {        
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.ListAsync(c => c.Id != Guid.Empty);
        }

        public async Task<User> GetById(Guid id)
        {
            return  await _userRepository.GetAsync(c => c.Id == id);
        }

        public async Task PutUser(UserViewModel userVM)
        {
            await _userRepository.UpdateAsync(userVM.MapToUser());
        }

        public async Task<User> PostUser(UserViewModel userVM)
        {
            return await _userRepository.AddAsync(userVM.MapToUser());
        }

        public async Task<User> DeleteUser(Guid id)
        {
            var user = await _userRepository.GetAsync(c => c.Id == id);
            if (user != null)
                await _userRepository.DeleteAsync(user);

            return user;
        }

    }
}