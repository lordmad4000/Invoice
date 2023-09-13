using MediatR;
using SimplexInvoice.Application.Common.Dto;
using System;

namespace SimplexInvoice.Application.IdDocumentTypes.Commands;

public record IdDocumentTypeUpdateCommand(Guid Id,
                                          string Name) : IRequest<IdDocumentTypeDto>;