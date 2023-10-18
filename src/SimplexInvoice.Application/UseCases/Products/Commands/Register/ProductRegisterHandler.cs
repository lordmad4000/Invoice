using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Products.Exceptions;
using SimplexInvoice.Domain.Products;
using SimplexInvoice.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.Products.Commands;
public class ProductRegisterHandler : IRequestHandler<ProductRegisterCommand, ProductDto>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;
    public ProductRegisterHandler(IProductRepository productRepository, 
                                 IMapper mapper, 
                                 ICustomLogger logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ProductDto> Handle(ProductRegisterCommand request, CancellationToken cancellationToken)
    {
        Product product = Product.Create(request.Code,
                                         request.Name,
                                         request.Description,
                                         request.PackageQuantity,
                                         new Money(request.Currency, request.Price),
                                         request.TaxRateId);   
        
        ProductDto productDto = _mapper.Map<ProductDto>(await _productRepository.AddAsync(product, cancellationToken));
        if (await _productRepository.SaveChangesAsync(cancellationToken) == 0)
            throw new ProductRegisteringException($"Error registering the Product.");

        _logger.Debug(@$"Product Registered successfully with data: {product}");

        return productDto;
    }
}