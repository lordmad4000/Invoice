using Invoice.Application.Common.Dto;
using MediatR;

namespace Invoice.Application.Authentication.Commands;

public record AuthenticationRegisterCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName) : IRequest<UserDto>;
