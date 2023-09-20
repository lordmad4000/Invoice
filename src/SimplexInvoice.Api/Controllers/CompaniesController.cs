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

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] CompanyRegisterRequest companyRegisterRequest, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            throw new Exception(ModelState.ToString());

        var companyRegisterCommand = _mapper.Map<CompanyRegisterCommand>(companyRegisterRequest);
        var companyDto = await _mediator.Send(companyRegisterCommand, cancellationToken);
        string url = GetByIdUrl(companyDto.Id);

        return (Created(url, companyDto));
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] CompanyUpdateRequest companyUpdateRequest, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            throw new Exception(ModelState.ToString());

        var companyUpdateCommand = _mapper.Map<CompanyUpdateCommand>(companyUpdateRequest);
        CompanyDto companyDto = await _mediator.Send(companyUpdateCommand, cancellationToken);

        return (Ok(companyDto));
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var companyRemoveCommand = new CompanyRemoveCommand(id);
        bool result = await _mediator.Send(companyRemoveCommand, cancellationToken);

        return (Ok(result));
    }

}