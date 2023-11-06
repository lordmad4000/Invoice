using MediatR;
using SimplexInvoice.Application.Common.Dto;

namespace SimplexInvoice.Application.Products.Queries;

public record GetProductByCodeQuery(string Code) : IRequest<ProductDto>;