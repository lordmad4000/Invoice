using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Companies.Exceptions;
using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.Companies.Commands;
public class CompanyUpdateHandler : IRequestHandler<CompanyUpdateCommand, CompanyDto>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;

    public CompanyUpdateHandler(ICompanyRepository companyRepository,
                               IMapper mapper,
                               ICustomLogger logger)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CompanyDto> Handle(CompanyUpdateCommand request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetAsync(c => c.Id == request.Id, cancellationToken, false)
            ?? throw new NotFoundException("Company not found.");

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

        CompanyDto companyDto = _mapper.Map<CompanyDto>(await _companyRepository.UpdateAsync(company, cancellationToken));
        if (await _companyRepository.SaveChangesAsync(cancellationToken) == 0)
            throw new CompanyUpdatingException($"Error updating the Company.");

        _logger.Debug(@$"Company Updated successfully with data: {company}");

        return companyDto;
    }

}