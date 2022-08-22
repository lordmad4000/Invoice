using AutoMapper;
using Invoice.Application.Common.Dto;
using Invoice.Application.Common.Interfaces.Persistance;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System;

namespace Invoice.Application.CQRS.Users.Queries
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IUserRepository userRepository,
                                    IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.ListAsync(c => c.Id != Guid.Empty);

            return _mapper.Map<List<UserDto>>(users);
        }
    }
}