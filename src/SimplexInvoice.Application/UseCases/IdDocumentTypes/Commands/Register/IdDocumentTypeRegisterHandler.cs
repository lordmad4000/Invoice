using AutoMapper;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Exceptions;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Domain.IdDocumentTypes;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace SimplexInvoice.Application.IdDocumentTypes.Commands.Register
{
    public class IdDocumentTypeRegisterHandler : IRequestHandler<IdDocumentTypeRegisterCommand, IdDocumentTypeDto>
    {
        private readonly IIdDocumentTypeRepository _idDocumentTypeRepository;
        private readonly IMapper _mapper;
        private readonly ICustomLogger _logger;

        public IdDocumentTypeRegisterHandler(IIdDocumentTypeRepository idDocumentTypeRepository,                        
                                             IMapper mapper,
                                             ICustomLogger logger)
        {
            _idDocumentTypeRepository = idDocumentTypeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IdDocumentTypeDto> Handle(IdDocumentTypeRegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _idDocumentTypeRepository.GetAsync(c => c.Name.ToLower() == request.Name.ToLower(), false) != null)
               throw new RegisterRuleValidationException("Name already exists.");

            var idDocumentType = IdDocumentType.Create(request.Name);
            idDocumentType = await _idDocumentTypeRepository.AddAsync(idDocumentType);
            await _idDocumentTypeRepository.SaveChangesAsync();
            _logger.Debug($"IdDocumentType Register with data: {idDocumentType}");

            await Task.Delay(10);

            return _mapper.Map<IdDocumentTypeDto>(idDocumentType);
        }

    }
}