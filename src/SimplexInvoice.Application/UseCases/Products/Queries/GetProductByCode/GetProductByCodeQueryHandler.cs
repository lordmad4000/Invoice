using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Common.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.Products.Queries;
public class GetProductByCodeQueryHandler : IRequestHandler<GetProductByCodeQuery, ProductDto>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;

    public GetProductByCodeQueryHandler(IProductRepository productRepository,
                                        IMapper mapper,
                                        ICustomLogger logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ProductDto> Handle(GetProductByCodeQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAsync(c => c.Code == request.Code, cancellationToken, true, $"Code=={request.Code}", new string[] { "TaxRate" });
        if (product is null)
            throw new NotFoundException($"Product with id {request.Code} was not found");

        _logger.Debug($"GetProductByCode with data: {product.ToString()}");

        return _mapper.Map<ProductDto>(product);
    }
}