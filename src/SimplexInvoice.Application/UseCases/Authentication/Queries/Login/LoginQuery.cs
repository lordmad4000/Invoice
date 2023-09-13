using SimplexInvoice.Application.Common.Dto;
using MediatR;

namespace SimplexInvoice.Application.Authentication.Queries;

public record LoginQuery(
    string Email,
    string Password) : IRequest<UserDto>;