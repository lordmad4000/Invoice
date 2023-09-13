using AutoMapper;
using SimplexInvoice.Application.IdDocumentTypes.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Api.Models.Request;
using SimplexInvoice.Application.IdDocumentTypes.Queries;

namespace SimplexInvoice.Api.Controllers;

[Authorize]
[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class IdDocumentTypesController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public IdDocumentTypesController(IMediator mediator,
                              IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("GetById{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetIdDocumentTypeByIdQuery(id);

        return (Ok(await _mediator.Send(query, cancellationToken)));
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetIdDocumentTypesQuery();
        var idDocumentTypesDto = await _mediator.Send(query, cancellationToken);

        return (Ok(idDocumentTypesDto));
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] IdDocumentTypeRegisterRequest idDocumentTypeRegisterRequest, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            throw new Exception(ModelState.ToString());

        var idDocumentTypeRegisterCommand = _mapper.Map<IdDocumentTypeRegisterCommand>(idDocumentTypeRegisterRequest);
        var idDocumentTypeDto = await _mediator.Send(idDocumentTypeRegisterCommand, cancellationToken);
        var url = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/{idDocumentTypeDto.Id}";

        return (Created(url, idDocumentTypeDto));
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] IdDocumentTypeDto idDocumentTypeDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            throw new Exception(ModelState.ToString());

        var idDocumentTypeUpdateCommand = _mapper.Map<IdDocumentTypeUpdateCommand>(idDocumentTypeDto);
        idDocumentTypeDto = await _mediator.Send(idDocumentTypeUpdateCommand, cancellationToken);

        return (Ok(idDocumentTypeDto));
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var idDocumentTypeRemoveCommand = new IdDocumentTypeRemoveCommand(id);
        bool result = await _mediator.Send(idDocumentTypeRemoveCommand, cancellationToken);

        return (Ok(result));
    }

}