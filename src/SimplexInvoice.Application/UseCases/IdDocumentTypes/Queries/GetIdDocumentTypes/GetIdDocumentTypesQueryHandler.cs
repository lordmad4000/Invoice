using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.IdDocumentTypes.Queries;
public class GetIdDocumentTypesQueryHandler : IRequestHandler<GetIdDocumentTypesQuery, ICollection<IdDocumentTypeDto>>
{
    private readonly IIdDocumentTypeRepository _idDocumentTypeRepository;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;

    public GetIdDocumentTypesQueryHandler(IIdDocumentTypeRepository idDocumentTypeRepository,
                                IMapper mapper,
                                ICustomLogger logger)
    {
        _idDocumentTypeRepository = idDocumentTypeRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ICollection<IdDocumentTypeDto>> Handle(GetIdDocumentTypesQuery request, CancellationToken cancellationToken)
    {
        var idDocumentTypes = await _idDocumentTypeRepository.ListAsync(c => c.Id != Guid.Empty, cancellationToken);
        var idDocumentTypesDto = _mapper.Map<List<IdDocumentTypeDto>>(idDocumentTypes);
        _logger.Debug($"GetIdDocumentTypes count: {idDocumentTypesDto.Count}");

        return idDocumentTypesDto;
    }
}