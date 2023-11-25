using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Interfaces;
using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.Users;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.Authentication.Commands
{

    public class AuthenticationRegisterHandler : IRequestHandler<AuthenticationRegisterCommand, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;
        private readonly ICustomLogger _logger;

        public AuthenticationRegisterHandler(IUnitOfWork unitOfWork,
                                             IUserRepository userRepository,                        
                                             IPasswordService passwordService,
                                             IMapper mapper,
                                             ICustomLogger logger)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _passwordService = passwordService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserDto> Handle(AuthenticationRegisterCommand request, CancellationToken cancellationToken)
        {            
            var user = await _userRepository.GetAsync(c => c.EmailAddress.Address == request.Email, cancellationToken, false);
            if (user != null)
                throw new BusinessRuleValidationException("Email address already exists.");

            var encryptedPassword = _passwordService.GeneratePassword(request.Email, request.Password, 16);
            user = User.Create(request.Email, encryptedPassword, request.FirstName, request.LastName);
            user = await _userRepository.AddAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.Debug($"Authentication Register with data: {user}");

            return _mapper.Map<UserDto>(user);
        }

    }
}