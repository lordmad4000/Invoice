using MediatR;
using SimplexInvoice.Application.Common.Dto;
using System;

namespace SimplexInvoice.Application.Customers.Queries;

public record GetCustomerByIdQuery(Guid Id) : IRequest<CustomerDto>;