using AutoMapper;
using SimplexInvoice.Application.Products.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Api.Models.Request;
using SimplexInvoice.Application.Products.Queries;

namespace SimplexInvoice.Api.Controllers;

[Authorize]
[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public ProductsController(IMediator mediator,
                              IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("GetById{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery(id);

        return (Ok(await _mediator.Send(query, cancellationToken)));
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetProductsQuery();
        var productsDto = await _mediator.Send(query, cancellationToken);

        return (Ok(productsDto));
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] ProductRegisterRequest productRegisterRequest, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            throw new Exception(ModelState.ToString());

        var productRegisterCommand = _mapper.Map<ProductRegisterCommand>(productRegisterRequest);
        var productDto = await _mediator.Send(productRegisterCommand, cancellationToken);
        var url = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/{productDto.Id}";

        return (Created(url, productDto));
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] ProductDto productDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            throw new Exception(ModelState.ToString());

        var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDto);
        productDto = await _mediator.Send(productUpdateCommand, cancellationToken);

        return (Ok(productDto));
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var productRemoveCommand = new ProductRemoveCommand(id);
        bool result = await _mediator.Send(productRemoveCommand, cancellationToken);

        return (Ok(result));
    }

}