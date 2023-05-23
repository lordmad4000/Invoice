using Invoice.Application.Common.Dto;
using MediatR;
using System;

namespace Invoice.Application.CQRS.Users.Commands;

public record UserRemoveCommand(Guid Id) : IRequest<UserDto>;