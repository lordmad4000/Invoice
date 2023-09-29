using AutoMapper;
using SimplexInvoice.Application.Customers.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimplexInvoice.Api.Models.Request;
using SimplexInvoice.Application.Customers.Queries;
using SimplexInvoice.Application.Common.Dto;

namespace SimplexInvoice.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CustomersController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public CustomersController(IMediator mediator,
                              IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("GetById{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCustomerByIdQuery(id);

        return (Ok(await _mediator.Send(query, cancellationToken)));
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetCustomersQuery();
        var customersDto = await _mediator.Send(query, cancellationToken);

        return (Ok(customersDto));
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] CustomerRegisterRequest customerRegisterRequest, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            throw new Exception(ModelState.ToString());

        var customerRegisterCommand = _mapper.Map<CustomerRegisterCommand>(customerRegisterRequest);
        var customerDto = await _mediator.Send(customerRegisterCommand, cancellationToken);
        string url = GetByIdUrl(customerDto.Id);

        return (Created(url, customerDto));
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] CustomerUpdateRequest customerUpdateRequest, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            throw new Exception(ModelState.ToString());

        var customerUpdateCommand = _mapper.Map<CustomerUpdateCommand>(customerUpdateRequest);
        var customerDto = await _mediator.Send(customerUpdateCommand, cancellationToken);

        return (Ok(customerDto));
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var customerRemoveCommand = new CustomerRemoveCommand(id);
        bool result = await _mediator.Send(customerRemoveCommand, cancellationToken);

        return (Ok(result));
    }

}