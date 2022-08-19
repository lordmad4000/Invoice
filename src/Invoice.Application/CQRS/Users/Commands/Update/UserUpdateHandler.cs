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
using System;

namespace Invoice.Application.CQRS.Users.Commands.Register
{

    public class UserUpdateHandler : IRequestHandler<UserUpdateCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidatorService _validatorService;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;

        public UserUpdateHandler(IUserRepository userRepository,                        
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

        public async Task<UserDto> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {            
            var user = await _userRepository.GetAsync(c => c.Id == request.Id, false);
            if (user == null)
                throw new Exception("User doesn't exists.");

            Validate(request);
            var encryptedPassword = _passwordService.GeneratePassword(request.Email, request.Password, 16);
            user.Update(new EmailAddress(request.Email), encryptedPassword, request.FirstName, request.LastName);
            await _userRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }

        private void Validate(UserUpdateCommand request)
        {
            var user = new User(new EmailAddress(request.Email),
                                request.Password, 
                                request.FirstName, 
                                request.LastName);
            _validatorService.ValidateModel(new UpdateUserValidator().Validate(user));                                
        }

    }
}