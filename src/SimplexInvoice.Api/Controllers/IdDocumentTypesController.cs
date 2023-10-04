using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimplexInvoice.Api.Models.Request;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.IdDocumentTypes.Commands;
using SimplexInvoice.Application.IdDocumentTypes.Queries;

namespace SimplexInvoice.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class IdDocumentTypesController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;
    public IdDocumentTypesController(IMediator mediator,
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
        var query = new GetIdDocumentTypeByIdQuery(id);
        IdDocumentTypeDto idDocumentTypeDto = await _mediator.Send(query, cancellationToken);

        return Ok(idDocumentTypeDto);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetIdDocumentTypesQuery();
        var idDocumentTypesDto = await _mediator.Send(query, cancellationToken);

        return Ok(idDocumentTypesDto);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] IdDocumentTypeRegisterRequest idDocumentTypeRegisterRequest, CancellationToken cancellationToken)
    {
        EnsureModelStateIsValid();
        var idDocumentTypeRegisterCommand = _mapper.Map<IdDocumentTypeRegisterCommand>(idDocumentTypeRegisterRequest);
        var idDocumentTypeDto = await _mediator.Send(idDocumentTypeRegisterCommand, cancellationToken);
        string url = GetByIdUrl(idDocumentTypeDto.Id);

        return Created(url, idDocumentTypeDto);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] IdDocumentTypeDto idDocumentTypeDto, CancellationToken cancellationToken)
    {
        EnsureModelStateIsValid();
        var idDocumentTypeUpdateCommand = _mapper.Map<IdDocumentTypeUpdateCommand>(idDocumentTypeDto);
        idDocumentTypeDto = await _mediator.Send(idDocumentTypeUpdateCommand, cancellationToken);

        return Ok(idDocumentTypeDto);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var idDocumentTypeRemoveCommand = new IdDocumentTypeRemoveCommand(id);
        bool result = await _mediator.Send(idDocumentTypeRemoveCommand, cancellationToken);

        return Ok(result);
    }

}