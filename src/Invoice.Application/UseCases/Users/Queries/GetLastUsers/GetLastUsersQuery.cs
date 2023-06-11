using Invoice.Application.Common.Dto;
using MediatR;
using System.Collections.Generic;

namespace Invoice.Application.Users.Queries;

public record GetLastUsersQuery(int Count) : IRequest<List<UserDto>>;