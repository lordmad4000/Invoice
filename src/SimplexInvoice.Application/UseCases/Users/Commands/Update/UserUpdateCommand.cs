using MediatR;
using SimplexInvoice.Application.Common.Dto;
using System;

namespace SimplexInvoice.Application.Users.Commands;

public record UserUpdateCommand(
    Guid Id,
    string Email,
    string Password,
    string FirstName,
    string LastName) : IRequest<UserDto>;