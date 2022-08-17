using System.Threading.Tasks;
using Invoice.Application.Interfaces;
using Invoice.Domain.Entities;
using Invoice.Domain.Exceptions;
using Invoice.Application.Common.Interfaces.Persistance;
using AutoMapper;
using Invoice.Application.Services.Configuration.Common;

namespace Invoice.Application.Services.Configuration.Commands
{

    public class IdDocumentTypeCommandService : IIdDocumentTypeCommandService
    {
        private readonly IIdDocumentTypeRepository _idDocumentTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidatorService _validatorService;
        private readonly IMapper _mapper;

        public IdDocumentTypeCommandService(IIdDocumentTypeRepository idDocumentTypeRepository,                        
                                            IUnitOfWork unitOfWork,
                                            IValidatorService validatorService,
                                            IMapper mapper)
        {
            _idDocumentTypeRepository = idDocumentTypeRepository;
            _unitOfWork = unitOfWork;
            _validatorService = validatorService;
            _mapper = mapper;
        }


        public async Task<IdDocumentTypeResult> Register(string name)
        {
            if (await _idDocumentTypeRepository.GetAsync(c => c.Name.ToLower() == name.ToLower(), false) != null)
                throw new EntityValidationException("Name already exists.");

            var idDocumentType = new IdDocumentType(name);
            // _validatorService.ValidateModel(new RegisterUserValidator().Validate(user));
            // var password = _passwordService.GeneratePassword(user.EmailAddress.ToString(), user.Password, 16);
            // user = new User(user.EmailAddress, password, user.FirstName, user.LastName);
            // user = await _userRepository.AddAsync(user);
            // await _unitOfWork.SaveChangesAsync();

            idDocumentType = await _idDocumentTypeRepository.AddAsync(idDocumentType);
            await _unitOfWork.SaveChangesAsync();

            return  _mapper.Map<IdDocumentTypeResult>(idDocumentType);
        }


    }
}