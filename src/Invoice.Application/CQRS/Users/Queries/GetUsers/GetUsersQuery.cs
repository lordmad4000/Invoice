using Invoice.Application.Common.Dto;
using MediatR;
using System.Collections.Generic;

namespace Invoice.Application.CQRS.Users.Queries;

public record GetUsersQuery() :  IRequest<List<UserDto>>;