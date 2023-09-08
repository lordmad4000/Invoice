using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.Companies.Queries;
public class GetCompaniesQueryHandler : IRequestHandler<GetCompaniesQuery, ICollection<CompanyDto>>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;

    public GetCompaniesQueryHandler(ICompanyRepository companyRepository,
                                IMapper mapper,
                                ICustomLogger logger)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ICollection<CompanyDto>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
    {
        var companies = await _companyRepository.ListAsync(c => c.Id != Guid.Empty, cancellationToken, true, new string[] { "IdDocumentType" });
        var companiesDto = _mapper.Map<List<CompanyDto>>(companies);
        _logger.Debug($"GetCompanies count: {companiesDto.Count}");

        return companiesDto;
    }
}