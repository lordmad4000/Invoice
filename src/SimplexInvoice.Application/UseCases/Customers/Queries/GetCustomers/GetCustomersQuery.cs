using MediatR;
using SimplexInvoice.Application.Common.Dto;
using System.Collections.Generic;

namespace SimplexInvoice.Application.Customers.Queries;

public record GetCustomersQuery() :  IRequest<ICollection<CustomerDto>>;