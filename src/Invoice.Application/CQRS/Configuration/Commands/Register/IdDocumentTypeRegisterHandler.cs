using System.Threading.Tasks;
using Invoice.Application.Interfaces;
using Invoice.Domain.Entities;
using Invoice.Domain.Exceptions;
using Invoice.Application.Common.Interfaces.Persistance;
using AutoMapper;
using MediatR;
using System.Threading;
using Invoice.Application.Common.Dto;

namespace Invoice.Application.CQRS.Configuration.Commands.Register
{
    public class IdDocumentTypeRegisterHandler : IRequestHandler<IdDocumentTypeRegisterCommand, IdDocumentTypeDto>
    {
        private readonly IIdDocumentTypeRepository _idDocumentTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidatorService _validatorService;
        private readonly IMapper _mapper;

        public IdDocumentTypeRegisterHandler(IIdDocumentTypeRepository idDocumentTypeRepository,                        
                                             IUnitOfWork unitOfWork,
                                             IValidatorService validatorService,
                                             IMapper mapper)
        {
            _idDocumentTypeRepository = idDocumentTypeRepository;
            _unitOfWork = unitOfWork;
            _validatorService = validatorService;
            _mapper = mapper;
        }

        public async Task<IdDocumentTypeDto> Handle(IdDocumentTypeRegisterCommand request, CancellationToken cancellationToken)
        {
            // TODO VALIDATION
            if (await _idDocumentTypeRepository.GetAsync(c => c.Name.ToLower() == request.Name.ToLower(), false) != null)
                throw new EntityValidationException("Name already exists.");

            var idDocumentType = new IdDocumentType(request.Name);
            idDocumentType = await _idDocumentTypeRepository.AddAsync(idDocumentType);
            await _unitOfWork.SaveChangesAsync();

            return  _mapper.Map<IdDocumentTypeDto>(idDocumentType);
        }

    }
}