using SimplexInvoice.Application.Common.Dto;
using MediatR;

namespace SimplexInvoice.Application.Authentication.Commands;

public record AuthenticationRegisterCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName) : IRequest<UserDto>;
