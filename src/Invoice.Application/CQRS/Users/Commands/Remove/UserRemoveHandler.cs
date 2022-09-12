using System.Threading.Tasks;
using Invoice.Application.Interfaces;
using Invoice.Domain.Entities;
using Invoice.Application.Common.Interfaces.Persistance;
using AutoMapper;
using MediatR;
using System.Threading;
using Invoice.Domain.Validations;
using Invoice.Application.Common.Dto;
using System;

namespace Invoice.Application.CQRS.Users.Commands
{

    public class UserRemoveHandler : IRequestHandler<UserRemoveCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidatorService _validatorService;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;

        public UserRemoveHandler(IUserRepository userRepository,                        
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

        public async Task<UserDto> Handle(UserRemoveCommand request, CancellationToken cancellationToken)
        {
            var user = await Validate(request);
            if (!await _userRepository.DeleteAsync(user.Id))
                return null;                
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }

        private async Task<User> Validate(UserRemoveCommand request)
        {
            var user = await _userRepository.GetAsync(c => c.Id == request.Id, false);
            if (user == null)
                throw new Exception("User doesn't exists.");
            _validatorService.ValidateModel(new UpdateUserValidator().Validate(user));

            return user;
        }

    }
}