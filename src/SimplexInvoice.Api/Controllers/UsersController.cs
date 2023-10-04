using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SimplexInvoice.Api.Models.Request;
using SimplexInvoice.Api.Models.Response;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Users.Commands;
using SimplexInvoice.Application.Users.Queries;

namespace SimplexInvoice.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;
    public UsersController(IMediator mediator,
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
        var query = new GetUserByIdQuery(id);
        var userDto = await _mediator.Send(query, cancellationToken);

        return Ok(_mapper.Map<UserResponse>(userDto));
    }

    [HttpGet("GetLast{count}")]
    public async Task<IActionResult> GetLast(int count, CancellationToken cancellationToken)
    {
        var query = new GetLastUsersQuery(count);
        var usersDto = await _mediator.Send(query, cancellationToken);

        return Ok(_mapper.Map<List<UserResponse>>(usersDto));
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetUsersQuery();
        var usersDto = await _mediator.Send(query, cancellationToken);

        return Ok(_mapper.Map<List<UserResponse>>(usersDto));
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequest userRegisterRequest, CancellationToken cancellationToken)
    {
        EnsureModelStateIsValid();
        var userRegisterCommand = _mapper.Map<UserRegisterCommand>(userRegisterRequest);
        var userDto = await _mediator.Send(userRegisterCommand, cancellationToken);
        string url = GetByIdUrl(userDto.Id);

        return Created(url, _mapper.Map<UserResponse>(userDto));
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] UserUpdateRequest userUpdateRequest, CancellationToken cancellationToken)
    {
        EnsureModelStateIsValid();
        var userUpdateCommand = _mapper.Map<UserUpdateCommand>(userUpdateRequest);
        var userDto = await _mediator.Send(userUpdateCommand, cancellationToken);

        return Ok(_mapper.Map<UserResponse>(userDto));
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var userRemoveCommand = new UserRemoveCommand(id);
        bool result = await _mediator.Send(userRemoveCommand, cancellationToken);

        return Ok(result);
    }

    [HttpPatch("PatchReplaceById/{id}")]
    public async Task<IActionResult> PatchReplaceById([FromBody] JsonPatchDocument<UserDto> patchDoc, Guid id, CancellationToken cancellationToken)
    {
        if (patchDoc is null)
            throw new Exception("No field to update provided.");
        // ONLY ALLOWED REPLACE OPERATIONS
        patchDoc.Operations.RemoveAll(c => c.op != "replace");
        if (patchDoc.Operations.Count == 0)
            throw new Exception("No field to update provided.");

        var query = new GetUserByIdQuery(id);
        var userDto = await _mediator.Send(query, cancellationToken);
        patchDoc.ApplyTo(userDto, ModelState);
        EnsureModelStateIsValid();
        var userUpdateCommand = _mapper.Map<UserUpdateCommand>(userDto);
        userDto = await _mediator.Send(userUpdateCommand);

        return Ok(_mapper.Map<UserResponse>(userDto));
    }
}
