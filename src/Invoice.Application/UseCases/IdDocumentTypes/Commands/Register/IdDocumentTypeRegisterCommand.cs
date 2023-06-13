using MediatR;
using Invoice.Application.Common.Dto;

namespace Invoice.Application.IdDocumentTypes.Commands.Register;


public record IdDocumentTypeRegisterCommand(string Name) : IRequest<IdDocumentTypeDto>;