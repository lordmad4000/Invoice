using AutoMapper;
using Invoice.Application.Common.Dto;
using Invoice.Application.Common.Interfaces.Persistance;
using Invoice.Application.Interfaces;
using Invoice.Domain.Exceptions;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Invoice.Application.Users.Commands
{

    public class UserUpdateHandler : IRequestHandler<UserUpdateCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;
        private readonly ICustomLogger _logger;

        public UserUpdateHandler(IUserRepository userRepository,
                                 IUnitOfWork unitOfWork,
                                 IPasswordService passwordService,
                                 IMapper mapper,
                                 ICustomLogger logger)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserDto> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(c => c.Id == request.Id, false) 
                ?? throw new NotFoundException("User not found.");

            var encryptedPassword = _passwordService.GeneratePassword(request.Email, request.Password, 16);
            user.Update(request.Email, encryptedPassword, request.FirstName, request.LastName);
            await _userRepository.UpdateAsync(user);
            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new Exception("User could not be updated succesfully.");

            _logger.Debug(@$"User Update with data: {user}");

            return _mapper.Map<UserDto>(user);
        }

    }
}