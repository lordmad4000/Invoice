using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Companies.Exceptions;
using SimplexInvoice.Domain.Companies;
using SimplexInvoice.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.Companies.Commands;
public class CompanyRegisterHandler : IRequestHandler<CompanyRegisterCommand, CompanyDto>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;
    public CompanyRegisterHandler(ICompanyRepository companyRepository, 
                                  IMapper mapper, 
                                  ICustomLogger logger)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CompanyDto> Handle(CompanyRegisterCommand request, CancellationToken cancellationToken)
    {
        Company company = Company.Create(request.Name,                                         
                                         request.IdDocumentTypeId,
                                         request.IdDocumentNumber,
                                         new Address(request.Street,
                                                     request.City,
                                                     request.State,
                                                     request.Country,
                                                     request.PostalCode),
                                         new PhoneNumber(request.Phone),
                                         new EmailAddress(request.Email));

        CompanyDto companyDto = _mapper.Map<CompanyDto>(await _companyRepository.AddAsync(company, cancellationToken));
        if (await _companyRepository.SaveChangesAsync(cancellationToken) == 0)
            throw new CompanyRegisteringException($"Error registering the Company.");

        _logger.Debug(@$"Company Registered successfully with data: {company}");

        return companyDto;
    }
}