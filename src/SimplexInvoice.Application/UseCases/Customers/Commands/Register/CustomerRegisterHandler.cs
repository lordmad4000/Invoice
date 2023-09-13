using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Customers.Exceptions;
using SimplexInvoice.Domain.Customers;
using SimplexInvoice.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.Customers.Commands;
public class CustomerRegisterHandler : IRequestHandler<CustomerRegisterCommand, CustomerDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;
    public CustomerRegisterHandler(ICustomerRepository customerRepository, 
                                   IMapper mapper, 
                                   ICustomLogger logger)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CustomerDto> Handle(CustomerRegisterCommand request, CancellationToken cancellationToken)
    {
        Customer customer = Customer.Create(request.FirstName,
                                            request.LastName,
                                            request.IdDocumentTypeId,
                                            request.IdDocumentNumber,
                                            new Address(request.Street,
                                                        request.City,
                                                        request.State,
                                                        request.Country,
                                                        request.PostalCode),
                                            new PhoneNumber(request.Phone),
                                            new EmailAddress(request.Email));

        CustomerDto customerDto = _mapper.Map<CustomerDto>(await _customerRepository.AddAsync(customer, cancellationToken));
        if (await _customerRepository.SaveChangesAsync(cancellationToken) == 0)
            throw new CustomerRegisteringException($"Error registering the Customer.");

        _logger.Debug(@$"Customer Registered successfully with data: {customer}");

        return customerDto;
    }
}