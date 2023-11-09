using MediatR;
using SimplexInvoice.Application.Common.Models;
using System.Collections.Generic;

namespace SimplexInvoice.Application.Products.Queries;

public record GetBasicProductsContainsNameQuery(string Name) :  IRequest<ICollection<BasicProduct>>;