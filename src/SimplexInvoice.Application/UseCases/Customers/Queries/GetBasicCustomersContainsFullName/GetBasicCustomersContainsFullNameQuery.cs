using MediatR;
using SimplexInvoice.Application.Common.Models;
using System.Collections.Generic;

namespace SimplexInvoice.Application.Customers.Queries;

public record GetBasicCustomersContainsFullNameQuery(string FullName) :  IRequest<ICollection<BasicCustomer>>;