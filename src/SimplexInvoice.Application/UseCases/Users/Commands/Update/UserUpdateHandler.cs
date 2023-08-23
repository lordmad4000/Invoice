using AutoMapper;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Interfaces;
using SimplexInvoice.Domain.Exceptions;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace SimplexInvoice.Application.Users.Commands
{

    public class UserUpdateHandler : IRequestHandler<UserUpdateCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;
        private readonly ICustomLogger _logger;

        public UserUpdateHandler(IUserRepository userRepository,
                                 IPasswordService passwordService,
                                 IMapper mapper,
                                 ICustomLogger logger)
        {
            _userRepository = userRepository;
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
            if (await _userRepository.SaveChangesAsync() == 0)
                throw new Exception("User could not be updated succesfully.");

            _logger.Debug(@$"User Update with data: {user}");

            return _mapper.Map<UserDto>(user);
        }

    }
}