using MediatR;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.IdDocumentTypes.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.IdDocumentTypes.Commands;

public class IdDocumentTypeRemoveHandler : IRequestHandler<IdDocumentTypeRemoveCommand, bool>
{
    private readonly IIdDocumentTypeRepository _idDocumentTypeRepository;
    private readonly ICustomLogger _logger;

    public IdDocumentTypeRemoveHandler(IIdDocumentTypeRepository idDocumentTypeRepository,
                               ICustomLogger logger)
    {
        _idDocumentTypeRepository = idDocumentTypeRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(IdDocumentTypeRemoveCommand request, CancellationToken cancellationToken)
    {
        await _idDocumentTypeRepository.DeleteAsync(request.Id, cancellationToken);
        if (await _idDocumentTypeRepository.SaveChangesAsync(cancellationToken) == 0)
            throw new IdDocumentTypeRemovingException($"Error removing the IdDocumentType.");

        _logger.Debug(@$"IdDocumentType with id {request.Id} removed.");

        return true;
    }
}