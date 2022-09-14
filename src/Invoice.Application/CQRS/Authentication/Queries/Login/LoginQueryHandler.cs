using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Invoice.Application.Common.Dto;
using Invoice.Application.Common.Interfaces.Persistance;
using Invoice.Application.Interfaces;
using MediatR;

namespace Invoice.Application.CQRS.Authentication.Queries
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, UserDto>
    {

        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;

        public LoginQueryHandler(IUserRepository userRepository,
                                 IPasswordService passwordService,
                                 IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(c => c.EmailAddress.Address == request.Email, false);
            if (user == null || _passwordService.IsCorrectPassword(user.EmailAddress.ToString(), user.Password, request.Password) == false)
                return null;

            return _mapper.Map<UserDto>(user);
        }        

    }
}