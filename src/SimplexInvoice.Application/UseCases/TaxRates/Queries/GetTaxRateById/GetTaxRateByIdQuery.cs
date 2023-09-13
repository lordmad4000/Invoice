using MediatR;
using SimplexInvoice.Application.Common.Dto;
using System;

namespace SimplexInvoice.Application.TaxRates.Queries;

public record GetTaxRateByIdQuery(Guid Id) : IRequest<TaxRateDto>;