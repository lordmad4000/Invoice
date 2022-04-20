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
        private readonly INotificationService _notificationService;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, INotificationService notificationService)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
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
            var user = await _userRepository.GetAsync(c => c.Id == userVM.Id, false);
            if (user != null)
            {
                user.Update(userVM.UserName, userVM.Password, userVM.FirstName, userVM.LastName);
                await _userRepository.UpdateAsync(user);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<UserViewModel> PostUser(UserViewModel userVM)
        {
            var user = MapUserViewModelToUser(userVM);
            user = await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
            await _notificationService.SendAsync(userVM, user.ActivationCode);

            return MapUserToUserViewModel(user);
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var result = await _userRepository.DeleteAsync(id);
            if (result)
                await _unitOfWork.SaveChangesAsync();

            return result;
        }

        public async Task<bool> ActivateUser(string activationCode)
        {
            var user = await _userRepository.GetAsync(c => c.ActivationCode == activationCode && c.Active == false, false);
            if (user == null)
                return false;

            user.Activate();
            await _userRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ValidateCredentials(string username, string password)
        {
            var user = await _userRepository.GetAsync(c => c.UserName == username && c.Password == password && c.Active == true);
            if (user == null)
                return false;

            return true;
        }

        // TODO AGREGAR AUTOMAPPER Y QUITAR
        private User MapUserViewModelToUser(UserViewModel userVM)
        {
            var user = new User(userVM.UserName, userVM.Password, userVM.FirstName, userVM.LastName, new EmailAddress(userVM.Email));

            return user;
        }
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