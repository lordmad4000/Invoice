using MediatR;
using SimplexInvoice.Application.Common.Dto;
using System.Collections.Generic;

namespace SimplexInvoice.Application.Products.Queries;

public record GetProductsQuery() :  IRequest<ICollection<ProductDto>>;