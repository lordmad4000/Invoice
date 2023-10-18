using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.Products.Queries;
public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ICollection<ProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;

    public GetProductsQueryHandler(IProductRepository productRepository,
                                IMapper mapper,
                                ICustomLogger logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ICollection<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.ListAsync(c => c.Id != Guid.Empty, cancellationToken, true, new string[] { "TaxRate" });
        var productsDto = _mapper.Map<List<ProductDto>>(products);
        _logger.Debug($"GetProducts count: {productsDto.Count}");

        return productsDto;
    }
}