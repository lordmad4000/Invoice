using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimplexInvoice.Api.Models.Request;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Exceptions;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Companies.Commands;
using SimplexInvoice.Application.Companies.Queries;

namespace SimplexInvoice.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;
    public CompaniesController(IMediator mediator,
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
        var query = new GetCompanyByIdQuery(id);
        CompanyDto companyDto = await _mediator.Send(query, cancellationToken);
        if (companyDto is null)
            throw new NotFoundException($"Company with id {id} was not found");

        return Ok(companyDto);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetCompaniesQuery();
        var companiesDto = await _mediator.Send(query, cancellationToken);

        return Ok(companiesDto);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> RegisterUpdate([FromBody] CompanyRegisterUpdateRequest CompanyRegisterUpdateRequest, CancellationToken cancellationToken)
    {
        EnsureModelStateIsValid();
        var CompanyRegisterUpdateCommand = _mapper.Map<CompanyRegisterUpdateCommand>(CompanyRegisterUpdateRequest);
        CompanyDto companyDto = await _mediator.Send(CompanyRegisterUpdateCommand, cancellationToken);

        return Ok(companyDto);
    }

}