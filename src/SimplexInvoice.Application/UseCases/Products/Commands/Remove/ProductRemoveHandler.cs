using MediatR;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Products.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.Products.Commands;

public class ProductRemoveHandler : IRequestHandler<ProductRemoveCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly ICustomLogger _logger;

    public ProductRemoveHandler(IUnitOfWork unitOfWork, 
                                IProductRepository productRepository,
                                ICustomLogger logger)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
    {
        await _productRepository.DeleteAsync(request.Id, cancellationToken);
        if (await _unitOfWork.SaveChangesAsync(cancellationToken) == 0)
            throw new ProductRemovingException($"Error removing the Product.");

        _logger.Debug(@$"Product with id {request.Id} removed.");

        return true;
    }
}