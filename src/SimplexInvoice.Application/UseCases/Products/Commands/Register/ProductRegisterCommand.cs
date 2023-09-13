using SimplexInvoice.Application.Common.Dto;
using MediatR;
using System;

namespace SimplexInvoice.Application.Products.Commands;

public record ProductRegisterCommand(string Code,
                                     string Name,
                                     string Description,
                                     double PackageQuantity,
                                     double Price,
                                     string Currency,
                                     Guid ProductTaxRateId) : IRequest<ProductDto>;