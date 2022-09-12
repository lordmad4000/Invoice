using AutoMapper;
using Invoice.Application.Common.Dto;
using Invoice.Application.Common.Interfaces.Persistance;
using Invoice.Application.Interfaces;
using Invoice.Domain.Entities;
using Invoice.Domain.Validations;
using Invoice.Domain.ValueObjects;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Invoice.Application.CQRS.Users.Commands
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
            User user = await Validate(request);
            var encryptedPassword = _passwordService.GeneratePassword(request.Email, request.Password, 16);
            user.Update(user.EmailAddress, encryptedPassword, user.FirstName, user.LastName);
            await _userRepository.UpdateAsync(user);
            if (await _unitOfWork.SaveChangesAsync() == 0)
                return null;

            return _mapper.Map<UserDto>(user);
        }

        private async Task<User> Validate(UserUpdateCommand request)
        {
            var user = await _userRepository.GetAsync(c => c.Id == request.Id, false);
            if (user == null)
                throw new Exception("User doesn't exists.");
            user.Update(new EmailAddress(request.Email), request.Password, request.FirstName, request.LastName);
            _validatorService.ValidateModel(new UpdateUserValidator().Validate(user));

            return user;
        }

    }
}