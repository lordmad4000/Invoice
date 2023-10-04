using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Common.Exceptions;
using System.Threading.Tasks;
using System.Threading;

namespace SimplexInvoice.Application.Companies.Queries;
public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, CompanyDto>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;

    public GetCompanyByIdQueryHandler(ICompanyRepository companyRepository,
                                     IMapper mapper,
                                     ICustomLogger logger)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CompanyDto> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetAsync(c => c.Id == request.Id, cancellationToken, true, $"Id=={request.Id}", new string[] { "IdDocumentType" });
        if (company is null)
            throw new NotFoundException($"Company with id {request.Id} was not found");

        _logger.Debug($"GetCompanyById with data: {company.ToString()}");

        return _mapper.Map<CompanyDto>(company);
    }
}