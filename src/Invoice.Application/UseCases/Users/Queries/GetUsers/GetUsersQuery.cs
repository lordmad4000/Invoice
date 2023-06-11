using Invoice.Application.Common.Dto;
using MediatR;
using System.Collections.Generic;

namespace Invoice.Application.Users.Queries;

public record GetUsersQuery() :  IRequest<List<UserDto>>;