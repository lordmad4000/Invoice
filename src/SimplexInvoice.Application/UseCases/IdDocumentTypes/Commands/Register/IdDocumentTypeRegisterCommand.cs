using MediatR;
using SimplexInvoice.Application.Common.Dto;

namespace SimplexInvoice.Application.IdDocumentTypes.Commands;


public record IdDocumentTypeRegisterCommand(string Name) : IRequest<IdDocumentTypeDto>;