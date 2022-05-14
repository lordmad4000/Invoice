using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Users.API.Models.Request;
using Users.API.Models.Response;
using Users.Application.Interfaces;
using Users.Application.Models;
using Users.CrossCutting.Configuration;

namespace Users.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JWTConfig _jwtConfig;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IOptions<JWTConfig> jwtConfig, IMapper mapper)
        {
            _userService = userService;
            _jwtConfig = jwtConfig.Value;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userService.GetUsers();
                return (Ok(_mapper.Map<List<UserResponse>> (users)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            try
            {
                var userDto = await _userService.GetById(id);
                return (Ok(_mapper.Map<UserResponse> (userDto)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutUser([FromBody] UserDto userDto)
        {
            try
            {
                await _userService.PutUser(userDto);
                return (Ok(_mapper.Map<UserResponse> (userDto)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] UserDto userDto)
        {
            try
            {
                userDto = await _userService.PostUser(userDto);
                return (Created("", userDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var result = await _userService.DeleteUser(id);
                return (Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("ActivateUser")]
        public async Task<IActionResult> ActivateUser()
        {
            try
            {
                string activationCode = "";
                var result = await _userService.ActivateUser(activationCode);
                return (Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest userLoginRequest)
        {
            try
            {
                var userDto = await _userService.Login(userLoginRequest.Username, userLoginRequest.Password);
                if (userDto == null)
                    return BadRequest("Invalid Username or Password.");

                var userLoginResponse = new UserLoginResponse
                {
                    Id = userDto.Id,
                    Token = _userService.GetToken(userDto.Password, userDto.Email, _jwtConfig.SecretKey)
                };

                return (await Task.FromResult(Ok(userLoginResponse)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // TODO
        // [HttpPatch]

    }
}
