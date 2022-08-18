using System.Threading.Tasks;
using Invoice.Application.Interfaces;
using Invoice.Domain.Entities;
using Invoice.Domain.Exceptions;
using Invoice.Application.Common.Interfaces.Persistance;
using AutoMapper;
using MediatR;
using System.Threading;
using Invoice.Domain.ValueObjects;
using Invoice.Domain.Validations;
using Invoice.Application.Common.Dto;

namespace Invoice.Application.CQRS.Authentication.Commands.Register
{

    public class AuthenticationRegisterHandler : IRequestHandler<AuthenticationRegisterCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidatorService _validatorService;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;

        public AuthenticationRegisterHandler(IUserRepository userRepository,                        
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

        public async Task<UserDto> Handle(AuthenticationRegisterCommand request, CancellationToken cancellationToken)
        {            
            if (await _userRepository.GetAsync(c => c.EmailAddress.Address == request.Email, false) != null)
                throw new EntityValidationException("Email address already exists.");

            var encryptedPassword = _passwordService.GeneratePassword(request.Email, request.Password, 16);
            var user = new User(new EmailAddress(request.Email), encryptedPassword, request.FirstName, request.LastName);
            _validatorService.ValidateModel(new RegisterUserValidator().Validate(user));
            user = await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }

    }
}