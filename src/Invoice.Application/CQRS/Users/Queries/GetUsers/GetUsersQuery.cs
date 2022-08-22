using Invoice.Application.Common.Dto;
using MediatR;
using System.Collections.Generic;

namespace Invoice.Application.CQRS.Users.Queries
{
    public class GetUsersQuery : IRequest<List<UserDto>>
    {
    }
}