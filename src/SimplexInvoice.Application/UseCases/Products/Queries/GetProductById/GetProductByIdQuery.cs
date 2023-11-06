using MediatR;
using SimplexInvoice.Application.Common.Dto;
using System;

namespace SimplexInvoice.Application.Products.Queries;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>;