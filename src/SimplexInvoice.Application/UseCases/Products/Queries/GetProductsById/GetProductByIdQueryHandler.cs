using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Domain.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.Products.Queries;
public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;

    public GetProductByIdQueryHandler(IProductRepository productRepository,
                                     IMapper mapper,
                                     ICustomLogger logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAsync(c => c.Id == request.Id, cancellationToken, true, $"Id=={request.Id}", new string[] { "ProductTaxRate" });
        if (product is null)
            throw new NotFoundException($"Product with id {request.Id} was not found");

        _logger.Debug($"GetProductById with data: {product.ToString()}");

        return _mapper.Map<ProductDto>(product);
    }
}