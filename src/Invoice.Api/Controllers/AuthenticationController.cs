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

namespace Invoice.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly ITokenService _tokenService;
        private readonly JWTConfig _jwtConfig;

        public AuthenticationController(ILoginService loginService,
                                        ITokenService tokenService,
                                        IOptions<JWTConfig> jwtConfig,
                                        IMapper mapper)
        {
            _loginService = loginService;
            _tokenService = tokenService;
            _jwtConfig = jwtConfig.Value;
        }

        [AllowAnonymous]
        [HttpGet("Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                var userDto = await _loginService.Login(username, password);
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

    }
}
