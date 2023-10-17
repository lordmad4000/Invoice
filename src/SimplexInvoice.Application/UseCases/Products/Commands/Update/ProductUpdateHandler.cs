using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Products.Exceptions;
using SimplexInvoice.Application.Common.Exceptions;
using SimplexInvoice.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.Products.Commands;
public class ProductUpdateHandler : IRequestHandler<ProductUpdateCommand, ProductDto>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;

    public ProductUpdateHandler(IProductRepository productRepository,
                               IMapper mapper,
                               ICustomLogger logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ProductDto> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAsync(c => c.Id == request.Id, cancellationToken, false)
            ?? throw new NotFoundException("Product not found.");

        product.Update(request.Code,
                       request.Name,
                       request.Description,
                       request.PackageQuantity,
                       new Money(request.Currency, request.Price),
                       request.TaxRateId);

        ProductDto productDto = _mapper.Map<ProductDto>(await _productRepository.UpdateAsync(product, cancellationToken));
        if (await _productRepository.SaveChangesAsync(cancellationToken) == 0)
            throw new ProductUpdatingException($"Error updating the Product.");

        _logger.Debug(@$"Product Updated successfully with data: {product}");

        return productDto;
    }

}