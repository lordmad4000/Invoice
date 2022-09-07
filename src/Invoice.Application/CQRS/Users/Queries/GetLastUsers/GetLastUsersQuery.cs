using Invoice.Application.Common.Dto;
using MediatR;
using System.Collections.Generic;

namespace Invoice.Application.CQRS.Users.Queries
{
    public class GetLastUsersQuery : IRequest<List<UserDto>>
    {
        public int Count {get; private set; }
        public GetLastUsersQuery(int count)
        {
            Count = count;
        }
        
    }
}