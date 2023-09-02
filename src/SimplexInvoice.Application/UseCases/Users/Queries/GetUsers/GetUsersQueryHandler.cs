using AutoMapper;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;

namespace SimplexInvoice.Application.Users.Queries
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICustomLogger _logger;

        public GetUsersQueryHandler(IUserRepository userRepository,
                                    IMapper mapper,
                                    ICustomLogger logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.ListAsync(c => c.Id != Guid.Empty);            
            var usersDto = _mapper.Map<List<UserDto>>(users.ToList());
            _logger.Debug($"GetUsers count: {usersDto.Count}");

            return usersDto;
        }
    }
}