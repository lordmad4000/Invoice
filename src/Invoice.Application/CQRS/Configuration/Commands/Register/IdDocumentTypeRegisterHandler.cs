using AutoMapper;
using Invoice.Application.Common.Dto;
using Invoice.Application.Common.Interfaces.Persistance;
using Invoice.Application.Interfaces;
using Invoice.Domain.Entities;
using Invoice.Domain.Exceptions;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Invoice.Application.CQRS.Configuration.Commands.Register
{
    public class IdDocumentTypeRegisterHandler : IRequestHandler<IdDocumentTypeRegisterCommand, IdDocumentTypeDto>
    {
        private readonly IIdDocumentTypeRepository _idDocumentTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidatorService _validatorService;
        private readonly IMapper _mapper;
        private readonly ICustomLogger _logger;

        public IdDocumentTypeRegisterHandler(IIdDocumentTypeRepository idDocumentTypeRepository,                        
                                             IUnitOfWork unitOfWork,
                                             IValidatorService validatorService,
                                             IMapper mapper,
                                             ICustomLogger logger)
        {
            _idDocumentTypeRepository = idDocumentTypeRepository;
            _unitOfWork = unitOfWork;
            _validatorService = validatorService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IdDocumentTypeDto> Handle(IdDocumentTypeRegisterCommand request, CancellationToken cancellationToken)
        {
            // TODO VALIDATION
            if (await _idDocumentTypeRepository.GetAsync(c => c.Name.ToLower() == request.Name.ToLower(), false) != null)
                throw new EntityValidationException("Name already exists.");

            var idDocumentType = new IdDocumentType(request.Name);
            idDocumentType = await _idDocumentTypeRepository.AddAsync(idDocumentType);
            await _unitOfWork.SaveChangesAsync();
            _logger.Debug($"IdDocumentType Register with data: {idDocumentType.ToString()}");

            return  _mapper.Map<IdDocumentTypeDto>(idDocumentType);
        }

    }
}