using MediatR;
using System;

namespace SimplexInvoice.Application.TaxRates.Commands;

public record TaxRateRemoveCommand(Guid Id) : IRequest<bool>;