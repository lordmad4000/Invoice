using MediatR;
using System;

namespace SimplexInvoice.Application.Products.Commands;

public record ProductRemoveCommand(Guid Id) : IRequest<bool>;