using AutoMapper;
using SimplexInvoice.Application.Companies.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Api.Models.Request;
using SimplexInvoice.Application.Companies.Queries;

namespace SimplexInvoice.Api.Controllers;

[Authorize]
[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public CompaniesController(IMediator mediator,
                              IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("GetById{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCompanyByIdQuery(id);

        return (Ok(await _mediator.Send(query, cancellationToken)));
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetCompaniesQuery();
        var companiesDto = await _mediator.Send(query, cancellationToken);

        return (Ok(companiesDto));
    }

    [HttpPut("Update")]
    public async Task<IActionResult> RegisterUpdate([FromBody] CompanyRegisterUpdateRequest CompanyRegisterUpdateRequest, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            throw new Exception(ModelState.ToString());

        var CompanyRegisterUpdateCommand = _mapper.Map<CompanyRegisterUpdateCommand>(CompanyRegisterUpdateRequest);
        CompanyDto companyDto = await _mediator.Send(CompanyRegisterUpdateCommand, cancellationToken);

        return (Ok(companyDto));
    }

}