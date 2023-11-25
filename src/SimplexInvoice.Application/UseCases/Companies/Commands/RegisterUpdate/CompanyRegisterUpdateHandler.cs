using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Companies.Exceptions;
using SimplexInvoice.Domain.Companies;
using SimplexInvoice.Domain.ValueObjects;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.Companies.Commands;
public class CompanyRegisterUpdateHandler : IRequestHandler<CompanyRegisterUpdateCommand, CompanyDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;

    public CompanyRegisterUpdateHandler(IUnitOfWork unitOfWork,
                                        ICompanyRepository companyRepository,
                                        IMapper mapper,
                                        ICustomLogger logger)
    {
        _unitOfWork = unitOfWork;
        _companyRepository = companyRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CompanyDto> Handle(CompanyRegisterUpdateCommand request, CancellationToken cancellationToken)
    {
        CompanyDto companyDto = new CompanyDto();
        Company company = (await _companyRepository.ListAsync(c => c.Id != Guid.Empty, cancellationToken))
                                                   .FirstOrDefault();                     
        if (company is null)
        {
            company = Company.Create(request.Name,
                                     request.IdDocumentTypeId,
                                     request.IdDocumentNumber,
                                     new Address(request.Street,
                                                 request.City,
                                                 request.State,
                                                 request.Country,
                                                 request.PostalCode),
                                     new PhoneNumber(request.Phone),
                                     new EmailAddress(request.Email));
            companyDto = _mapper.Map<CompanyDto>(await _companyRepository.AddAsync(company, cancellationToken));
        }
        else
        {
            company.Update(request.Name,
                           request.IdDocumentTypeId,
                           request.IdDocumentNumber,
                           new Address(request.Street,
                                       request.City,
                                       request.State,
                                       request.Country,
                                       request.PostalCode),
                           new PhoneNumber(request.Phone),
                           new EmailAddress(request.Email));
            companyDto = _mapper.Map<CompanyDto>(await _companyRepository.UpdateAsync(company, cancellationToken));
        }
        if (await _unitOfWork.SaveChangesAsync(cancellationToken) == 0)
            throw new CompanyRegisteringUpdatingException($"Error updating the Company.");

        _logger.Debug(@$"Company Updated successfully with data: {company}");

        return companyDto;
    }

}