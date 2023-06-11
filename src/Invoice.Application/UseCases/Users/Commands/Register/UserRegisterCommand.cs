using Invoice.Application.Common.Dto;
using MediatR;

namespace Invoice.Application.Users.Commands;

public record UserRegisterCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName) : IRequest<UserDto>;