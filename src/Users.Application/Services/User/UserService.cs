using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Application.Interfaces;
using Users.Domain.Entities;
using Users.Domain.Interfaces;
using Users.Domain.ValueObjects;

namespace Users.Application.Services
{

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
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
            if (await _userRepository.GetAsync(c => c.Id == userVM.Id, false) != null)
            {
                var user = MapUserViewModelToUser(userVM);
                await _userRepository.UpdateAsync(MapUserViewModelToUser(userVM));
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<UserViewModel> PostUser(UserViewModel userVM)
        {
            userVM.Id = Guid.NewGuid();
            var user = await _userRepository.AddAsync(MapUserViewModelToUser(userVM));
            await _unitOfWork.SaveChangesAsync();
            return MapUserToUserViewModel(user);
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var result = await _userRepository.DeleteAsync(id);
            if (result)
                await _unitOfWork.SaveChangesAsync();

            return result;
        }

        // TODO AGREGAR AUTOMAPPER Y QUITAR
        private User MapUserViewModelToUser(UserViewModel userVM) => new User(userVM.Id, userVM.UserName, userVM.Password, userVM.FirstName, userVM.LastName, new EmailAddress(userVM.Email));
        private UserViewModel MapUserToUserViewModel(User user)
        {
            return new UserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.EmailAddress.ToString()
            };
        }

    }
}