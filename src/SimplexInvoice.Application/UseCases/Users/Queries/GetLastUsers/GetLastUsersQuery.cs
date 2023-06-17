using SimplexInvoice.Application.Common.Dto;
using MediatR;
using System.Collections.Generic;

namespace SimplexInvoice.Application.Users.Queries;

public record GetLastUsersQuery(int Count) : IRequest<List<UserDto>>;