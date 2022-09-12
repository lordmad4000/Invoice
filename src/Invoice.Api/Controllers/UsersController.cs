using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Invoice.Api.Models.Request;
using Invoice.Api.Models.Response;
using Invoice.Application.Interfaces;
using Invoice.Domain.Exceptions;
using Invoice.Infra.Exceptions;
using Invoice.Application.Common.Dto;
using MediatR;
using Invoice.Application.CQRS.Users.Queries;
using Invoice.Application.CQRS.Users.Commands;
using System.Linq;

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
            try
            {
                var query =  new GetUserByIdQuery(id);
                var userDto = await _mediator.Send(query);
                if (userDto == null)
                    return NotFound(new { errorMessage = $"User with id {id} was not found" });

                return (Ok(_mapper.Map<UserResponse>(userDto)));
            }
            catch (DataBaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpGet("GetLast{count}")]
        public async Task<IActionResult> GetLast(int count)
        {
            try
            {
                var query =  new GetLastUsersQuery(count);
                var usersDto = await _mediator.Send(query);

                return (Ok(_mapper.Map<List<UserResponse>>(usersDto)));
            }
            catch (DataBaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var query =  new  GetUsersQuery();
                var usersDto = await _mediator.Send(query);

                return (Ok(_mapper.Map<List<UserResponse>>(usersDto)));
            }
            catch (DataBaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UserUpdateRequest userUpdateRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var userUpdateCommand =  _mapper.Map<UserUpdateCommand>(userUpdateRequest);
                var userDto = await _mediator.Send(userUpdateCommand);

                return (Ok(_mapper.Map<UserResponse>(userDto)));
            }
            catch (EntityValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DataBaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var userRemoveCommand =  new UserRemoveCommand(id);
                bool result = await _mediator.Send(userRemoveCommand) == null ? false : true;

                return (Ok(result));
            }
            catch (DataBaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest userRegisterRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var userRegisterCommand = _mapper.Map<UserRegisterCommand> (userRegisterRequest);
                var userDto = await _mediator.Send(userRegisterCommand);
                var url = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/{userDto.Id}";
                
                return (Created(url, _mapper.Map<UserResponse>(userDto)));
            }
            catch (EntityValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DataBaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPatch("PatchReplaceById/{id}")]
        public async Task<IActionResult> PatchReplaceById([FromBody] JsonPatchDocument<UserDto> patchDoc, Guid id)
        {
            try
            {
                if (patchDoc == null)
                    return BadRequest("No field to update provided.");

                // ONLY ALLOWED REPLACE OPERATIONS
                patchDoc.Operations.RemoveAll(c => c.op != "replace");

                if (patchDoc.Operations.Count == 0)
                    return BadRequest("No field to update provided.");

                var userDto = await _userService.GetById(id, false);

                if (userDto == null)
                    return NotFound();

                patchDoc.ApplyTo(userDto, ModelState);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userUpdateCommand =  _mapper.Map<UserUpdateCommand>(userDto);
                userDto = await _mediator.Send(userUpdateCommand);

                return (Ok(_mapper.Map<UserResponse>(userDto)));
            }
            catch (EntityValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DataBaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

    }
}
