using MediatR;
using SimplexInvoice.Application.Common.Dto;
using System;

namespace SimplexInvoice.Application.Products.Commands;

public record ProductUpdateCommand(Guid Id,
                                   string Code,
                                   string Name,
                                   string Description,
                                   double PackageQuantity,
                                   double Price,
                                   string Currency,
                                   Guid TaxRateId) : IRequest<ProductDto>;