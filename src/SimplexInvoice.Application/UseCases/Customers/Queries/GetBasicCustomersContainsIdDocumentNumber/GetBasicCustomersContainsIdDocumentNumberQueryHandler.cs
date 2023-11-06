using MediatR;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.Customers.Queries;
public class GetBasicCustomersContainsIdDocumentNumberQueryHandler : IRequestHandler<GetBasicCustomersContainsIdDocumentNumberQuery, ICollection<BasicCustomer>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomLogger _logger;

    public GetBasicCustomersContainsIdDocumentNumberQueryHandler(ICustomerRepository customerRepository,
                                                           ICustomLogger logger)
    {
        _customerRepository = customerRepository;
        _logger = logger;
    }

    public async Task<ICollection<BasicCustomer>> Handle(GetBasicCustomersContainsIdDocumentNumberQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<BasicCustomer> basicCustomers = await _customerRepository.GetBasicCustomersContainsIdDocumentNumber(request.IdDocumentNumber, cancellationToken);
        _logger.Debug($"GetBasicCustomers count: {basicCustomers.Count()}");

        return basicCustomers.ToList();
    }
}