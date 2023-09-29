using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimplexInvoice.Api.Models.Request;
using SimplexInvoice.Application.Invoices.Commands;
using SimplexInvoice.Application.Invoices.Queries;

namespace SimplexInvoice.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public InvoicesController(IMediator mediator,
                              IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("GetById{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetInvoiceByIdQuery(id);

        return (Ok(await _mediator.Send(query, cancellationToken)));
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetInvoicesQuery();
        var invoicesDto = await _mediator.Send(query, cancellationToken);

        return (Ok(invoicesDto));
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] InvoiceRegisterRequest invoiceRegisterRequest, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            throw new Exception(ModelState.ToString());

        var invoiceRegisterCommand = _mapper.Map<InvoiceRegisterCommand>(invoiceRegisterRequest);
        var invoiceDto = await _mediator.Send(invoiceRegisterCommand, cancellationToken);
        string url = GetByIdUrl(invoiceDto.Id);

        return (Created(url, invoiceDto));
    }

}