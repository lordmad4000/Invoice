using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Domain.Exceptions;
using System.Threading.Tasks;
using System.Threading;

namespace SimplexInvoice.Application.Customers.Queries;
public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository,
                                     IMapper mapper,
                                     ICustomLogger logger)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetAsync(c => c.Id == request.Id, cancellationToken, true, $"Id=={request.Id}", new string[] { "IdDocumentType" });
        if (customer is null)
            throw new NotFoundException($"Customer with id {request.Id} was not found");

        _logger.Debug($"GetCustomerById with data: {customer.ToString()}");

        return _mapper.Map<CustomerDto>(customer);
    }
}