using MediatR;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Common.Models;
using SimplexInvoice.Application.Products.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.Products.Queries;
public class GetBasicProductsContainsNameQueryHandler : IRequestHandler<GetBasicProductsContainsNameQuery, ICollection<BasicProduct>>
{
    private readonly IProductRepository _productRepository;
    private readonly ICustomLogger _logger;

    public GetBasicProductsContainsNameQueryHandler(IProductRepository productRepository,
                                                    ICustomLogger logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task<ICollection<BasicProduct>> Handle(GetBasicProductsContainsNameQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<BasicProduct> basicProducts = await _productRepository.GetBasicProductsContainsName(request.Name, cancellationToken);
        _logger.Debug($"GetBasicProductsContainsNameQuery count: {basicProducts.Count()}");

        return basicProducts.ToList();
    }

}