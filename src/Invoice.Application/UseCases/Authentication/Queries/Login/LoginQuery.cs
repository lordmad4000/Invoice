using Invoice.Application.Common.Dto;
using MediatR;

namespace Invoice.Application.Authentication.Queries;

public record LoginQuery(
    string Email,
    string Password) : IRequest<UserDto>;