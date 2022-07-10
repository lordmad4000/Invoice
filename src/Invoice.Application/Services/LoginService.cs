using System.Threading.Tasks;
using Invoice.Application.Interfaces;
using Invoice.Domain.Interfaces;
using Invoice.Application.Models;
using AutoMapper;
using Invoice.Domain.ValueObjects;

namespace Invoice.Application.Services
{

    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;

        public LoginService(IUserRepository userRepository,
                            IPasswordService passwordService,
                            IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _mapper = mapper;
        }


        public async Task<UserDto> Login(string email, string password)
        {
            var user = await _userRepository.GetAsync(c => c.EmailAddress.Address == email, false);

            if (user == null || _passwordService.IsCorrectPassword(user.EmailAddress.ToString(), user.Password, password) == false)
                return null;

            return _mapper.Map<UserDto>(user);
        }

    }
}