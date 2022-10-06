using AutoMapper;
using Invoice.Application.Common.Dto;
using Invoice.Application.Common.Interfaces.Persistance;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Invoice.Application.CQRS.Users.Queries
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
            _logger.Debug($"GetLastUsers count: {request.Count}");

            return _mapper.Map<List<UserDto>>(users);
        }
    }
}