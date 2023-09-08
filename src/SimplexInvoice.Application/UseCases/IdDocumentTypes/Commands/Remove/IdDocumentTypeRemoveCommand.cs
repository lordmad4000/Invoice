using MediatR;
using System;

namespace SimplexInvoice.Application.IdDocumentTypes.Commands;

public record IdDocumentTypeRemoveCommand(Guid Id) : IRequest<bool>;