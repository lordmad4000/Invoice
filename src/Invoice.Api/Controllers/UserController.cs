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
using Invoice.Application.Models;
using Invoice.Domain.Exceptions;
using Invoice.Infra.Exceptions;

namespace Invoice.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService,
                              IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var userDto = await _userService.GetById(id);                

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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var Invoice = await _userService.GetAll();

                return (Ok(_mapper.Map<List<UserResponse>>(Invoice)));
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

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserUpdateRequest userUpdateRequest)
        {
            try
            {
                var userDto = _mapper.Map<UserDto>(userUpdateRequest);
                await _userService.Update(userDto);

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

        [HttpPatch("PathReplaceUser/{id}")]
        public async Task<IActionResult> PatchReplace([FromBody] JsonPatchDocument<UserDto> patchDoc, Guid id)
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

                userDto = await _userService.PatchUpdate(userDto);

                return Ok(_mapper.Map<UserResponse>(userDto));
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var result = await _userService.Delete(id);
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

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserRegisterRequest userRegisterRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userDto = _mapper.Map<UserDto>(userRegisterRequest);
                userDto = await _userService.Register(userDto);
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

        //[AllowAnonymous]
        //[HttpPost("Login")]
        //public async Task<IActionResult> Login([FromBody] UserLoginRequest userLoginRequest)
        //{
        //    try
        //    {
        //        var userDto = await _userService.Login(userLoginRequest.Username, userLoginRequest.Password);
        //        if (userDto == null)
        //            return BadRequest("Invalid Username or Password.");

        //        var userLoginResponse = new UserLoginResponse
        //        {
        //            Id = userDto.Id,
        //            Token = _tokenService.GenerateToken(userDto.Password, userDto.Email, _jwtConfig.SecretKey)
        //        };

        //        return (await Task.FromResult(Ok(userLoginResponse)));
        //    }
        //    catch (DataBaseException ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.InnerException.Message);
        //    }
        //}

    }
}
