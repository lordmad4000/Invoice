using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Common.Exceptions;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;
using SimplexInvoice.Domain.Companies;

namespace SimplexInvoice.Application.Companies.Queries;
public class GetCompanyQueryHandler : IRequestHandler<GetCompanyQuery, CompanyDto>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;

    public GetCompanyQueryHandler(ICompanyRepository companyRepository,
                                  IMapper mapper,
                                  ICustomLogger logger)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CompanyDto> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
    {
        var companies = await _companyRepository.ListAsync(c => c.Id != Guid.Empty, cancellationToken, true, new string[] { "IdDocumentType" });
        if (companies is null)
            throw new NotFoundException($"Company was not found");

        Company company = companies.First();
        _logger.Debug($"GetCompany with data: {company.ToString()}");

        return _mapper.Map<CompanyDto>(company);
    }
}