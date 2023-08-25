using SimplexInvoice.Application.Common.Dto;
using MediatR;

namespace SimplexInvoice.Application.Users.Commands;

public record UserRegisterCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName) : IRequest<UserDto>;