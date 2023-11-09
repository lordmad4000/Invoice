using MediatR;
using SimplexInvoice.Application.Common.Models;
using System.Collections.Generic;

namespace SimplexInvoice.Application.Customers.Queries;

public record GetBasicCustomersContainsIdDocumentNumberQuery(string IdDocumentNumber) :  IRequest<ICollection<BasicCustomer>>;