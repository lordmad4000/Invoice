using MediatR;
using System;

namespace SimplexInvoice.Application.Companies.Commands;

public record CompanyRemoveCommand(Guid Id) : IRequest<bool>;