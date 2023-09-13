using MediatR;
using SimplexInvoice.Application.Common.Dto;
using System;

namespace SimplexInvoice.Application.IdDocumentTypes.Queries;

public record GetIdDocumentTypeByIdQuery(Guid Id) : IRequest<IdDocumentTypeDto>;