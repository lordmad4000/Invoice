using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Invoice.Application.Interfaces;
using Invoice.Domain.Entities;
using Invoice.Domain.Interfaces;
using Invoice.Domain.Validations;
using Invoice.Application.Models;
using AutoMapper;
using Invoice.Domain.ValueObjects;
using Invoice.Domain.Exceptions;

namespace Invoice.Application.Services
{

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidatorService _validatorService;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository,                        
                           IUnitOfWork unitOfWork,
                           IValidatorService validatorService,
                           IPasswordService passwordService,
                           IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _validatorService = validatorService;
            _passwordService = passwordService;
            _mapper = mapper;
        }

        public async Task<UserDto> GetById(Guid id, bool tracking = true)
        {
            var user = await _userRepository.GetAsync(c => c.Id == id, tracking, $"Id=={id}");

            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var usersDto = new List<UserDto>();
            var users = await _userRepository.ListAsync(c => c.Id != Guid.Empty);
            foreach (var user in users)
                usersDto.Add(_mapper.Map<UserDto>(user));

            return usersDto;
        }

        public async Task<UserDto> Register(UserDto userDto)
        {
            if (await _userRepository.GetAsync(c => c.EmailAddress.Address == userDto.Email, false) != null)
                throw new EntityValidationException("Email address already exists.");

            var user = _mapper.Map<User>(userDto);
            _validatorService.ValidateModel(new RegisterUserValidator().Validate(user));
            var password = _passwordService.GeneratePassword(user.EmailAddress.ToString(), user.Password, 16);
            user = new User(user.EmailAddress, password, user.FirstName, user.LastName);
            user = await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }

        public async Task Update(UserDto userDto)
        {
            var user = await _userRepository.GetAsync(c => c.Id == userDto.Id, false);
            if (user != null)
            {
                userDto.Password = user.Password;
                user = _mapper.Map<User>(userDto);
                _validatorService.ValidateModel(new UpdateUserValidator().Validate(user));
                user.Update(new EmailAddress(userDto.Email), userDto.Password, userDto.FirstName, userDto.LastName);
                await _userRepository.UpdateAsync(user);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<UserDto> PatchUpdate(UserDto userDto)
        {
            if (userDto != null)
            {
                var user = _mapper.Map<User>(userDto);
                var updateUserValidator = new UpdateUserValidator();
                var validator = updateUserValidator.Validate(user);
                _validatorService.ValidateModel(validator);                
                user.Update(new EmailAddress(userDto.Email), userDto.Password, userDto.FirstName, userDto.LastName);
                await _userRepository.UpdateAsync(user);
                await _unitOfWork.SaveChangesAsync();
            }

            return userDto;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _userRepository.DeleteAsync(id);
            if (result)
                await _unitOfWork.SaveChangesAsync();

            return result;
        }

    }
}