using SimplexInvoice.Application.Common.Dto;
using MediatR;
using System.Collections.Generic;

namespace SimplexInvoice.Application.Users.Queries;

public record GetUsersQuery() :  IRequest<List<UserDto>>;