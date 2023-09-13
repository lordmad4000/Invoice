using MediatR;
using SimplexInvoice.Application.Common.Dto;
using System.Collections.Generic;

namespace SimplexInvoice.Application.Invoices.Queries;

public record GetInvoicesQuery() :  IRequest<ICollection<InvoiceDto>>;