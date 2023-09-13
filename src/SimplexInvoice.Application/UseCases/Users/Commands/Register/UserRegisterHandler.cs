using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Interfaces;
using SimplexInvoice.Application.Users.Exceptions;
using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.Users;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.Users.Commands
{

    public class UserRegisterHandler : IRequestHandler<UserRegisterCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;
        private readonly ICustomLogger _logger;
        public UserRegisterHandler(IUserRepository userRepository,
                                   IPasswordService passwordService,
                                   IMapper mapper,
                                   ICustomLogger logger)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserDto> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(c => c.EmailAddress.Address == request.Email, cancellationToken, false);
            if (user is not null)
                throw new BusinessRuleValidationException("Email address already exists.");

            var encryptedPassword = _passwordService.GeneratePassword(request.Email, request.Password, 16);
            user = User.Create(request.Email, encryptedPassword, request.FirstName, request.LastName);
            UserDto userDto = _mapper.Map<UserDto>(await _userRepository.AddAsync(user, cancellationToken));
            if (await _userRepository.SaveChangesAsync(cancellationToken) == 0)
                throw new UserRegisteringException($"Error registering the User.");

            _logger.Debug(@$"User Registered successfully with data: {user}");

            return userDto;
        }

    }
}