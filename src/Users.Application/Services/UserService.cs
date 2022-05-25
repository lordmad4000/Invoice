using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Application.Interfaces;
using Users.Domain.Entities;
using Users.Domain.Interfaces;
using Users.Domain.Validations;
using Users.Domain.Exceptions;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Users.Application.Models;
using AutoMapper;

namespace Users.Application.Services
{

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;
        private readonly IPasswordEncryption _passwordEncryptionService;
        private readonly IValidatorService _validatorService;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository,                        
                           IUnitOfWork unitOfWork,
                           INotificationService notificationService,
                           IPasswordEncryption passwordEncryptionService,
                           IValidatorService validatorService,
                           IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
            _passwordEncryptionService = passwordEncryptionService;
            _validatorService = validatorService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var usersDto = new List<UserDto>();
            var users = await _userRepository.ListAsync(c => c.Id != Guid.Empty);
            foreach (var user in users)
                usersDto.Add(_mapper.Map<UserDto>(user));

            return usersDto;
        }

        public async Task<UserDto> GetById(Guid id, bool tracking = true)
        {
            var user = await _userRepository.GetAsync(c => c.Id == id, tracking);
            return _mapper.Map<UserDto>(user);
        }

        public async Task PutUser(UserDto userDto)
        {
            var user = await _userRepository.GetAsync(c => c.Id == userDto.Id, false);

            if (user != null)
            {
                userDto.Password = user.Password;
                user = _mapper.Map<User>(userDto);

                var updateUserValidator = new UpdateUserValidator();
                _validatorService.ValidateModel(updateUserValidator.Validate(user));

                user.Update(userDto.UserName, userDto.FirstName, userDto.LastName);
                await _userRepository.UpdateAsync(user);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<UserDto> PostUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            var registerUserValidator = new RegisterUserValidator();
            _validatorService.ValidateModel(registerUserValidator.Validate(user));

            var password = GenerateUserPassword(user);
            user = new User(user.UserName, password, user.FirstName, user.LastName, user.EmailAddress);

            user = await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
            await _notificationService.SendAsync(userDto, user.ActivationCode);

            return _mapper.Map<UserDto>(user);
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
            var user = await _userRepository.GetAsync(c => c.UserName == username && c.Active == true);

            if (user == null || IsCorrectPassword(user, password) == false)
                return null;

            return _mapper.Map<UserDto>(user);
        }

        public string GetToken(string userId, string userEmail, string secretKey)
        {
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Email, userEmail),
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<UserDto> PatchUser(UserDto userDto)
        {
            if (userDto != null)
            {
                var user = _mapper.Map<User>(userDto);

                var updateUserValidator = new UpdateUserValidator();
                var validator = updateUserValidator.Validate(user);
                _validatorService.ValidateModel(validator);
                
                user.Update(userDto.UserName, userDto.FirstName, userDto.LastName);
                await _userRepository.UpdateAsync(user);
                await _unitOfWork.SaveChangesAsync();
            }

            return userDto;
        }

        private string GenerateUserPassword(User user)
        {
            var salt = _passwordEncryptionService.GenerateSalt(16);
            var hash = _passwordEncryptionService.GenerateHash($"{user.UserName}{user.Password}", salt);

            return $"{salt},{hash}";
        }

        private bool IsCorrectPassword(User user, string password)
        {
            var splitSaltHashPassword = user.Password.Split(",");
            var hash = _passwordEncryptionService.GenerateHash($"{user.UserName}{password}", splitSaltHashPassword[0]);

            return hash.Equals(splitSaltHashPassword[1]);
        }

    }
}