using AutoMapper;
using Invoice.Application.Common.Dto;
using Invoice.Application.Common.Interfaces.Persistance;
using Invoice.Application.Interfaces;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Invoice.Application.Authentication.Queries
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, UserDto>
    {

        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;
        private readonly ICustomLogger _logger;

        public LoginQueryHandler(IUserRepository userRepository,
                                 IPasswordService passwordService,
                                 IMapper mapper,
                                 ICustomLogger logger)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(c => c.EmailAddress.Address == request.Email, false);
            if (user == null || _passwordService.IsCorrectPassword(user.EmailAddress.ToString(), user.Password, request.Password) == false)
                throw new System.Exception("Login error: Invalid Username or Password.");

            _logger.Debug($"Login with {request.Email} and {request.Password}");

            return _mapper.Map<UserDto>(user);
        }        

    }
}