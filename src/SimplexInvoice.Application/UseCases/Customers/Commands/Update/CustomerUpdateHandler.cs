using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Customers.Exceptions;
using SimplexInvoice.Application.Common.Exceptions;
using SimplexInvoice.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.Customers.Commands;
public class CustomerUpdateHandler : IRequestHandler<CustomerUpdateCommand, CustomerDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;

    public CustomerUpdateHandler(ICustomerRepository customerRepository,
                                 IMapper mapper,
                                 ICustomLogger logger)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CustomerDto> Handle(CustomerUpdateCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetAsync(c => c.Id == request.Id, cancellationToken, false)
            ?? throw new NotFoundException("Customer not found.");

        customer.Update(request.FirstName,
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

        CustomerDto customerDto = _mapper.Map<CustomerDto>(await _customerRepository.UpdateAsync(customer, cancellationToken));
        if (await _customerRepository.SaveChangesAsync(cancellationToken) == 0)
            throw new CustomerUpdatingException($"Error updating the Customer.");

        _logger.Debug(@$"Customer Updated successfully with data: {customer}");

        return customerDto;
    }

}