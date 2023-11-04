using MediatR;
using SimplexInvoice.Application.Common.Models;
using System.Collections.Generic;

namespace SimplexInvoice.Application.Customers.Queries;

public record GetBasicCustomersContainsEmailQuery(string Email) :  IRequest<ICollection<BasicCustomer>>;