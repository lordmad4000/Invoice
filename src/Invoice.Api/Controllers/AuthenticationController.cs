using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Invoice.Api.Configuration;
using Invoice.Api.Models.Response;
using Invoice.Application.Interfaces;
using Invoice.Infra.Exceptions;
using MediatR;
using Invoice.Api.Models.Request;
using Invoice.Application.CQRS.Authentication.Commands.Register;
using Invoice.Domain.Exceptions;
using Invoice.Application.CQRS.Authentication.Queries.Login;

namespace Invoice.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITokenService _tokenService;
        private readonly JWTConfig _jwtConfig;
        private readonly IMapper _mapper;

        public AuthenticationController(IMediator mediator,
                                        ITokenService tokenService,
                                        IOptions<JWTConfig> jwtConfig,
                                        IMapper mapper)
        {
            _mediator = mediator;
            _tokenService = tokenService;
            _jwtConfig = jwtConfig.Value;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var userDto = await _mediator.Send(new LoginQuery(email, password));
                if (userDto == null)
                    return BadRequest("Invalid Username or Password.");

                var userLoginResponse = new UserLoginResponse
                {
                    Id = userDto.Id,
                    Token = _tokenService.GenerateToken(userDto.Password, userDto.Email, _jwtConfig.SecretKey)
                };

                return (await Task.FromResult(Ok(userLoginResponse)));
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

                var authenticationRegisterCommand = new AuthenticationRegisterCommand(userRegisterRequest.Email, 
                                                                                      userRegisterRequest.Password, 
                                                                                      userRegisterRequest.FirstName, 
                                                                                      userRegisterRequest.LastName);
                var userDto = await _mediator.Send(authenticationRegisterCommand);
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


    }
}
