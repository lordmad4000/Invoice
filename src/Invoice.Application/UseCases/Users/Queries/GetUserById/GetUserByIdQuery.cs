using Invoice.Application.Common.Dto;
using MediatR;
using System;

namespace Invoice.Application.Users.Queries;

public record GetUserByIdQuery(Guid Id) : IRequest<UserDto>;