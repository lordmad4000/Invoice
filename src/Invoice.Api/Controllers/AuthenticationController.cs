using AutoMapper;
using Invoice.Api.Models.Request;
using Invoice.Api.Models.Response;
using Invoice.Application.CQRS.Authentication.Commands;
using Invoice.Application.CQRS.Authentication.Queries;
using Invoice.Application.Common.Interfaces.Persistance;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System;

namespace Invoice.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly ICustomLogger _logger;

        public AuthenticationController(IMediator mediator,
                                        ITokenService tokenService,
                                        IMapper mapper,
                                        ICustomLogger logger)
        {
            _mediator = mediator;
            _tokenService = tokenService;
            _mapper = mapper;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet("Login")]
        public async Task<IActionResult> Login(string email, [DataType(DataType.Password)] string password)
        {
            var userDto = await _mediator.Send(new LoginQuery(email, password));
            var userLoginResponse = new UserLoginResponse
            {
                Id = userDto.Id,
                Token = _tokenService.GenerateToken(userDto.Password, userDto.Email)
            };
            _logger.Debug($"Successfully logged in with token {userLoginResponse.Token}");

            return (await Task.FromResult(Ok(userLoginResponse)));
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest userRegisterRequest)
        {
            if (!ModelState.IsValid)
                throw new Exception(ModelState.ToString());

            var authenticationRegisterCommand = new AuthenticationRegisterCommand(userRegisterRequest.Email,
                                                                                  userRegisterRequest.Password,
                                                                                  userRegisterRequest.FirstName,
                                                                                  userRegisterRequest.LastName);
            var userDto = await _mediator.Send(authenticationRegisterCommand);
            var url = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/{userDto.Id}";

            return (Created(url, _mapper.Map<UserResponse>(userDto)));
        }
    }
}
