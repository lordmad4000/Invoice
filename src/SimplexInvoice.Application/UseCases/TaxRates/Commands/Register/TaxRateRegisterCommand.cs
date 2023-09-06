using MediatR;
using SimplexInvoice.Application.Common.Dto;

namespace SimplexInvoice.Application.TaxRates.Commands;

public record TaxRateRegisterCommand(string Name, 
                                     int Value) : IRequest<TaxRateDto>;