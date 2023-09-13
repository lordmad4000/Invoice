using MediatR;
using SimplexInvoice.Application.Common.Dto;
using System;

namespace SimplexInvoice.Application.Invoices.Queries;

public record GetInvoiceByIdQuery(Guid Id) : IRequest<InvoiceDto>;