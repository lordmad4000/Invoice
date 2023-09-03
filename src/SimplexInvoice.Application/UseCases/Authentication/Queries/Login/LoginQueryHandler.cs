using AutoMapper;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Interfaces;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace SimplexInvoice.Application.Authentication.Queries
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
            var user = await _userRepository.GetAsync(c => c.EmailAddress.Address == request.Email, cancellationToken, false);
            if (user is null || _passwordService.IsCorrectPassword(user.EmailAddress.ToString(), user.Password, request.Password) == false)
                throw new System.Exception("Login error: Invalid Username or Password.");

            _logger.Debug($"Login with {request.Email} and {request.Password}");

            return _mapper.Map<UserDto>(user);
        }        

    }
}