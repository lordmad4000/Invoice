using AutoMapper;
using Invoice.Api.Models.Request;
using Invoice.Api.Models.Response;
using Invoice.Application.CQRS.Users.Commands;
using Invoice.Application.CQRS.Users.Queries;
using Invoice.Application.Common.Dto;
using Invoice.Application.Interfaces;
using Invoice.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Invoice.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UsersController(IMediator mediator,
                               IUserService userService,
                               IMapper mapper)
        {
            _mediator = mediator;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("GetById{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetUserByIdQuery(id);
            var userDto = await _mediator.Send(query);
            if (userDto == null)
                throw new NotFoundException($"User with id {id} was not found");

            return (Ok(_mapper.Map<UserResponse>(userDto)));
        }

        [HttpGet("GetLast{count}")]
        public async Task<IActionResult> GetLast(int count)
        {
            var query = new GetLastUsersQuery(count);
            var usersDto = await _mediator.Send(query);

            return (Ok(_mapper.Map<List<UserResponse>>(usersDto)));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetUsersQuery();
            var usersDto = await _mediator.Send(query);

            return (Ok(_mapper.Map<List<UserResponse>>(usersDto)));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UserUpdateRequest userUpdateRequest)
        {
            if (!ModelState.IsValid)
                throw new Exception(ModelState.ToString());

            var userUpdateCommand = _mapper.Map<UserUpdateCommand>(userUpdateRequest);
            var userDto = await _mediator.Send(userUpdateCommand);

            return (Ok(_mapper.Map<UserResponse>(userDto)));
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userRemoveCommand = new UserRemoveCommand(id);
            bool result = await _mediator.Send(userRemoveCommand) == null ? false : true;

            return (Ok(result));
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest userRegisterRequest)
        {
            if (!ModelState.IsValid)
                throw new Exception(ModelState.ToString());

            var userRegisterCommand = _mapper.Map<UserRegisterCommand>(userRegisterRequest);
            var userDto = await _mediator.Send(userRegisterCommand);
            var url = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/{userDto.Id}";

            return (Created(url, _mapper.Map<UserResponse>(userDto)));
        }

        [HttpPatch("PatchReplaceById/{id}")]
        public async Task<IActionResult> PatchReplaceById([FromBody] JsonPatchDocument<UserDto> patchDoc, Guid id)
        {
            if (patchDoc == null)
                throw new Exception("No field to update provided.");

            // ONLY ALLOWED REPLACE OPERATIONS
            patchDoc.Operations.RemoveAll(c => c.op != "replace");

            if (patchDoc.Operations.Count == 0)
                throw new Exception("No field to update provided.");

            var userDto = await _userService.GetById(id, false);

            if (userDto == null)
                throw new NotFoundException("User not found.");

            patchDoc.ApplyTo(userDto, ModelState);

            if (!ModelState.IsValid)
                throw new Exception(ModelState.ToString());

            var userUpdateCommand = _mapper.Map<UserUpdateCommand>(userDto);
            userDto = await _mediator.Send(userUpdateCommand);

            return (Ok(_mapper.Map<UserResponse>(userDto)));
        }
    }
}
