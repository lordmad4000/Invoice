using Invoice.Application.Common.Dto;
using MediatR;
using System;

namespace Invoice.Application.Users.Commands;

public record UserUpdateCommand(
    Guid Id,
    string Email,
    string Password,
    string FirstName,
    string LastName) : IRequest<UserDto>;