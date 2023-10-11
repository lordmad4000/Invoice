using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimplexInvoice.Api.Models.Request;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
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
    private readonly ICustomLogger _logger;
    public InvoicesController(IMediator mediator,
                              IMapper mapper,
                              ICustomLogger logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("GetById{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetInvoiceByIdQuery(id);
        InvoiceDto invoiceDto = await _mediator.Send(query, cancellationToken);

        return Ok(invoiceDto);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetInvoicesQuery();
        var invoicesDto = await _mediator.Send(query, cancellationToken);

        return Ok(invoicesDto);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] InvoiceRegisterRequest invoiceRegisterRequest, CancellationToken cancellationToken)
    {
        EnsureModelStateIsValid();
        var invoiceRegisterCommand = _mapper.Map<InvoiceRegisterCommand>(invoiceRegisterRequest);
        var invoiceDto = await _mediator.Send(invoiceRegisterCommand, cancellationToken);
        string url = GetByIdUrl(invoiceDto.Id);

        return Created(url, invoiceDto);
    }

}