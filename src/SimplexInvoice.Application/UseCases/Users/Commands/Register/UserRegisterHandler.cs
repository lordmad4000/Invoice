using AutoMapper;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Interfaces;
using SimplexInvoice.Domain.Exceptions;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using SimplexInvoice.Domain.Users;

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
            var user = await _userRepository.GetAsync(c => c.EmailAddress.Address == request.Email, false);
            if (user is not null)
                throw new BusinessRuleValidationException("Email address already exists.");

            var encryptedPassword = _passwordService.GeneratePassword(request.Email, request.Password, 16);
            user = User.Create(request.Email, encryptedPassword, request.FirstName, request.LastName);
            user = await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            _logger.Debug(@$"User Register with data: {user}");

            return _mapper.Map<UserDto>(user);
        }

    }
}