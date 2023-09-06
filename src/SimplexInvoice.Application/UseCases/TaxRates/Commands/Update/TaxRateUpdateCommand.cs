using MediatR;
using SimplexInvoice.Application.Common.Dto;
using System;

namespace SimplexInvoice.Application.TaxRates.Commands;

public record TaxRateUpdateCommand(Guid Id, 
                                   string Name,
                                   int Value) : IRequest<TaxRateDto>;