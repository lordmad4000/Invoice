using SimplexInvoice.Application.Common.Dto;
using MediatR;
using System;

namespace SimplexInvoice.Application.Users.Commands;

public record UserRemoveCommand(Guid Id) : IRequest<bool>;