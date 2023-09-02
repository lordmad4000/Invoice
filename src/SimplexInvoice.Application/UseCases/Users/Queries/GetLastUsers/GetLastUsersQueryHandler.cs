using AutoMapper;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace SimplexInvoice.Application.Users.Queries
{
    public class GetLastUsersQueryHandler : IRequestHandler<GetLastUsersQuery, List<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICustomLogger _logger;

        public GetLastUsersQueryHandler(IUserRepository userRepository,
                                        IMapper mapper,
                                        ICustomLogger logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<UserDto>> Handle(GetLastUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetLastUsers(request.Count);
            var usersDto = _mapper.Map<List<UserDto>>(users);
            _logger.Debug($"GetLastUsers count: {usersDto.Count}");

            return usersDto;
        }
    }
}