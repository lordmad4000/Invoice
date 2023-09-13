using MediatR;
using SimplexInvoice.Application.Common.Dto;
using System.Collections.Generic;

namespace SimplexInvoice.Application.IdDocumentTypes.Queries;

public record GetIdDocumentTypesQuery() :  IRequest<ICollection<IdDocumentTypeDto>>;