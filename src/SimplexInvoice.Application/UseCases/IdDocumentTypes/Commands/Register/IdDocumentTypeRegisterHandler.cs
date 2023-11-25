using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.IdDocumentTypes.Exceptions;
using SimplexInvoice.Domain.IdDocumentTypes;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.IdDocumentTypes.Commands;
public class IdDocumentTypeRegisterHandler : IRequestHandler<IdDocumentTypeRegisterCommand, IdDocumentTypeDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIdDocumentTypeRepository _idDocumentTypeRepository;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;
    public IdDocumentTypeRegisterHandler(IUnitOfWork unitOfWork, 
                                         IIdDocumentTypeRepository idDocumentTypeRepository, 
                                         IMapper mapper, 
                                         ICustomLogger logger)
    {
        _unitOfWork = unitOfWork;
        _idDocumentTypeRepository = idDocumentTypeRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IdDocumentTypeDto> Handle(IdDocumentTypeRegisterCommand request, CancellationToken cancellationToken)
    {
        IdDocumentType idDocumentType = IdDocumentType.Create(request.Name);
        IdDocumentTypeDto idDocumentTypeDto = _mapper.Map<IdDocumentTypeDto>(await _idDocumentTypeRepository.AddAsync(idDocumentType, cancellationToken));
        if (await _unitOfWork.SaveChangesAsync(cancellationToken) == 0)
            throw new IdDocumentTypeRegisteringException($"Error registering the IdDocumentType.");

        _logger.Debug(@$"IdDocumentType Registered successfully with data: {idDocumentType}");

        return idDocumentTypeDto;
    }
}