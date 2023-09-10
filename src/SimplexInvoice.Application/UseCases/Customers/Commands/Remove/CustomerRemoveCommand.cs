using MediatR;
using System;

namespace SimplexInvoice.Application.Customers.Commands;

public record CustomerRemoveCommand(Guid Id) : IRequest<bool>;