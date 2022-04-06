using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Domain.Entities;
using Users.Domain.Interfaces;
using Users.Domain.ValueObjects;

namespace Users.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserViewModel>> GetUsers()
        {
            var usersVM = new List<UserViewModel>();
            var users = await _userRepository.ListAsync(c => c.Id != Guid.Empty);
            foreach (var user in users)
                usersVM.Add(MapUserToUserViewModel(user));

            return usersVM;
        }

        public async Task<UserViewModel> GetById(Guid id)
        {
            var user = await _userRepository.GetAsync(c => c.Id == id);
            return MapUserToUserViewModel(user);
        }

        public async Task PutUser(UserViewModel userVM)
        {
            await _userRepository.UpdateAsync(MapUserViewModelToUser(userVM));
        }

        public async Task<UserViewModel> PostUser(UserViewModel userVM)
        {
            var user = await _userRepository.AddAsync(MapUserViewModelToUser(userVM));
            return MapUserToUserViewModel(user);
        }

        public async Task<UserViewModel> DeleteUser(Guid id)
        {
            var user = await _userRepository.GetAsync(c => c.Id == id);
            if (user != null)
                await _userRepository.DeleteAsync(user);

            return MapUserToUserViewModel(user);
        }

        // TODO AGREGAR AUTOMAPPER Y QUITAR

        private User MapUserViewModelToUser(UserViewModel userVM) => new User(userVM.UserName, userVM.Password, userVM.FirstName, userVM.LastName, userVM.Email);
        private UserViewModel MapUserToUserViewModel(User user)
        {
            return new UserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.EmailAddress
            };
        }


    }
}