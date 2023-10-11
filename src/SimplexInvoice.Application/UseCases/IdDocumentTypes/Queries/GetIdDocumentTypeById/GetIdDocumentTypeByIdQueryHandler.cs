using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Common.Exceptions;
using System.Threading.Tasks;
using System.Threading;

namespace SimplexInvoice.Application.IdDocumentTypes.Queries;
public class GetIdDocumentTypeByIdQueryHandler : IRequestHandler<GetIdDocumentTypeByIdQuery, IdDocumentTypeDto>
{
    private readonly IIdDocumentTypeRepository _idDocumentTypeRepository;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;

    public GetIdDocumentTypeByIdQueryHandler(IIdDocumentTypeRepository idDocumentTypeRepository,
                                             IMapper mapper,
                                             ICustomLogger logger)
    {
        _idDocumentTypeRepository = idDocumentTypeRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IdDocumentTypeDto> Handle(GetIdDocumentTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var idDocumentType = await _idDocumentTypeRepository.GetAsync(c => c.Id == request.Id, cancellationToken, true, $"Id=={request.Id}");
        if (idDocumentType is null)
            throw new NotFoundException($"IdDocumentType with id {request.Id} was not found");

        _logger.Debug($"GetIdDocumentTypeById with data: {idDocumentType.ToString()}");

        return _mapper.Map<IdDocumentTypeDto>(idDocumentType);
    }
}