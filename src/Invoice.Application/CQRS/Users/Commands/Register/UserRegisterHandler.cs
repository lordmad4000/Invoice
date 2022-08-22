using AutoMapper;
using Invoice.Application.Common.Dto;
using Invoice.Application.Common.Interfaces.Persistance;
using Invoice.Application.Interfaces;
using Invoice.Domain.Entities;
using Invoice.Domain.Exceptions;
using Invoice.Domain.Validations;
using Invoice.Domain.ValueObjects;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Invoice.Application.CQRS.Users.Commands
{

    public class UserRegisterHandler : IRequestHandler<UserRegisterCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidatorService _validatorService;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;

        public UserRegisterHandler(IUserRepository userRepository,                        
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

        public async Task<UserDto> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {            
            await Validate(request);
            var encryptedPassword = _passwordService.GeneratePassword(request.Email, request.Password, 16);
            var user = new User(new EmailAddress(request.Email), encryptedPassword, request.FirstName, request.LastName);
            user = await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }

        private async Task Validate(UserRegisterCommand request)
        {
            if (await _userRepository.GetAsync(c => c.EmailAddress.Address == request.Email, false) != null)
                throw new EntityValidationException("Email address already exists.");

            var user = new User(new EmailAddress(request.Email),
                                request.Password, 
                                request.FirstName, 
                                request.LastName);
            _validatorService.ValidateModel(new RegisterUserValidator().Validate(user));
        }        

    }
}