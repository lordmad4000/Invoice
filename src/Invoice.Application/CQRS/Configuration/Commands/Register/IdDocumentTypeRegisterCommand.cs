using MediatR;
using Invoice.Application.Common.Dto;

namespace Invoice.Application.CQRS.Configuration.Commands.Register;


public record IdDocumentTypeRegisterCommand(string Name) : IRequest<IdDocumentTypeDto>;