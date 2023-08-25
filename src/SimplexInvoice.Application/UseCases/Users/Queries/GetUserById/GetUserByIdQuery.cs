using SimplexInvoice.Application.Common.Dto;
using MediatR;
using System;

namespace SimplexInvoice.Application.Users.Queries;

public record GetUserByIdQuery(Guid Id) : IRequest<UserDto>;