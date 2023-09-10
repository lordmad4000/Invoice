using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.Customers.Queries;
public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, ICollection<CustomerDto>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;

    public GetCustomersQueryHandler(ICustomerRepository customerRepository,
                                IMapper mapper,
                                ICustomLogger logger)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ICollection<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.ListAsync(c => c.Id != Guid.Empty, cancellationToken, true, new string[] { "IdDocumentType" });
        var customersDto = _mapper.Map<List<CustomerDto>>(customers);
        _logger.Debug($"GetCustomers count: {customersDto.Count}");

        return customersDto;
    }
}