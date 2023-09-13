using MediatR;
using SimplexInvoice.Application.Common.Dto;
using System.Collections.Generic;

namespace SimplexInvoice.Application.TaxRates.Queries;

public record GetTaxRatesQuery() :  IRequest<ICollection<TaxRateDto>>;