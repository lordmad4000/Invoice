using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Interfaces;
using SimplexInvoice.Application.Users.Exceptions;
using SimplexInvoice.Application.Common.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.Users.Commands
{

    public class UserUpdateHandler : IRequestHandler<UserUpdateCommand, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;
        private readonly ICustomLogger _logger;

        public UserUpdateHandler(IUnitOfWork unitOfWork,
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

        public async Task<UserDto> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(c => c.Id == request.Id, cancellationToken, false) 
                ?? throw new NotFoundException("User not found.");

            var encryptedPassword = _passwordService.GeneratePassword(request.Email, request.Password, 16);
            user.Update(request.Email, encryptedPassword, request.FirstName, request.LastName);
            UserDto userDto = _mapper.Map<UserDto>(await _userRepository.UpdateAsync(user, cancellationToken));
            if (await _unitOfWork.SaveChangesAsync(cancellationToken) == 0)
                throw new UserUpdatingException($"Error updating the User.");

            _logger.Debug(@$"User Updated successfully with data: {user}");

            return userDto;
        }

    }
}