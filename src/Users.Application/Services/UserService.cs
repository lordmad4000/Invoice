using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Application.Interfaces;
using Users.Domain.Entities;
using Users.Domain.Interfaces;
using Users.Domain.Validations;
using Users.Application.Models;
using AutoMapper;
using Users.Domain.ValueObjects;

namespace Users.Application.Services
{

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;
        private readonly IValidatorService _validatorService;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository,                        
                           IUnitOfWork unitOfWork,
                           INotificationService notificationService,
                           IValidatorService validatorService,
                           IPasswordService passwordService,
                           IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
            _validatorService = validatorService;
            _passwordService = passwordService;
            _mapper = mapper;
        }

        public async Task<UserDto> GetUserById(Guid id, bool tracking = true)
        {
            var user = await _userRepository.GetAsync(c => c.Id == id, tracking, $"Id=={id}");
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var usersDto = new List<UserDto>();
            var users = await _userRepository.ListAsync(c => c.Id != Guid.Empty);
            foreach (var user in users)
                usersDto.Add(_mapper.Map<UserDto>(user));

            return usersDto;
        }

        public async Task<UserDto> RegisterUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            var registerUserValidator = new RegisterUserValidator();
            _validatorService.ValidateModel(registerUserValidator.Validate(user));

            var password = _passwordService.GeneratePassword(user.UserName, user.Password, 16);
            user = new User(user.UserName, password, user.FirstName, user.LastName, user.EmailAddress);

            user = await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
            await _notificationService.SendAsync(userDto, user.ActivationCode);

            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdateUser(UserDto userDto)
        {
            var user = await _userRepository.GetAsync(c => c.Id == userDto.Id, false);

            if (user != null)
            {
                userDto.Password = user.Password;
                user = _mapper.Map<User>(userDto);

                var updateUserValidator = new UpdateUserValidator();
                _validatorService.ValidateModel(updateUserValidator.Validate(user));

                user.Update(userDto.UserName, userDto.FirstName, userDto.LastName, userDto.Password, new EmailAddress(userDto.Email));
                await _userRepository.UpdateAsync(user);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<UserDto> PatchUser(UserDto userDto)
        {
            if (userDto != null)
            {
                var user = _mapper.Map<User>(userDto);

                var updateUserValidator = new UpdateUserValidator();
                var validator = updateUserValidator.Validate(user);
                _validatorService.ValidateModel(validator);
                
                user.Update(userDto.UserName, userDto.FirstName, userDto.LastName, userDto.Password, new EmailAddress(userDto.Email));
                await _userRepository.UpdateAsync(user);
                await _unitOfWork.SaveChangesAsync();
            }

            return userDto;
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

        public async Task<UserDto> Login(string username, string password)
        {
            var user = await _userRepository.GetAsync(c => c.UserName == username && c.Active == true, false);

            if (user == null || _passwordService.IsCorrectPassword(user.UserName, user.Password, password) == false)
                return null;

            return _mapper.Map<UserDto>(user);
        }

    }
}