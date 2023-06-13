using AutoMapper;
using Invoice.Application.Common.Dto;
using Invoice.Application.Common.Exceptions;
using Invoice.Application.Common.Interfaces.Persistance;
using Invoice.Domain.IdDocumentTypes;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Invoice.Application.IdDocumentTypes.Commands.Register
{
    public class IdDocumentTypeRegisterHandler : IRequestHandler<IdDocumentTypeRegisterCommand, IdDocumentTypeDto>
    {
        private readonly IIdDocumentTypeRepository _idDocumentTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICustomLogger _logger;

        public IdDocumentTypeRegisterHandler(IIdDocumentTypeRepository idDocumentTypeRepository,                        
                                             IUnitOfWork unitOfWork,
                                             IMapper mapper,
                                             ICustomLogger logger)
        {
            _idDocumentTypeRepository = idDocumentTypeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IdDocumentTypeDto> Handle(IdDocumentTypeRegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _idDocumentTypeRepository.GetAsync(c => c.Name.ToLower() == request.Name.ToLower(), false) != null)
               throw new RegisterRuleValidationException("Name already exists.");

            var idDocumentType = IdDocumentType.Create(request.Name);
            idDocumentType = await _idDocumentTypeRepository.AddAsync(idDocumentType);
            await _unitOfWork.SaveChangesAsync();
            _logger.Debug($"IdDocumentType Register with data: {idDocumentType}");

            await Task.Delay(10);

            return _mapper.Map<IdDocumentTypeDto>(idDocumentType);
        }

    }
}