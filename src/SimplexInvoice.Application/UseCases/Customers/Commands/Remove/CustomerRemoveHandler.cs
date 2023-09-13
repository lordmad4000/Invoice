using MediatR;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Customers.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.Customers.Commands;

public class CustomerRemoveHandler : IRequestHandler<CustomerRemoveCommand, bool>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomLogger _logger;

    public CustomerRemoveHandler(ICustomerRepository customerRepository,
                               ICustomLogger logger)
    {
        _customerRepository = customerRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(CustomerRemoveCommand request, CancellationToken cancellationToken)
    {
        await _customerRepository.DeleteAsync(request.Id, cancellationToken);
        if (await _customerRepository.SaveChangesAsync(cancellationToken) == 0)
            throw new CustomerRemovingException($"Error removing the Customer.");

        _logger.Debug(@$"Customer with id {request.Id} removed.");

        return true;
    }
}