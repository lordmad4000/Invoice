using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimplexInvoice.Api.Models.Request;
using SimplexInvoice.Api.Models.Response;
using SimplexInvoice.Application.Authentication.Commands;
using SimplexInvoice.Application.Authentication.Queries;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using System.ComponentModel.DataAnnotations;

namespace SimplexInvoice.Api.Controllers;

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
    public async Task<IActionResult> Login(string email, [DataType(DataType.Password)] string password, CancellationToken cancellationToken)
    {
        var userDto = await _mediator.Send(new LoginQuery(email, password), cancellationToken);
        var userLoginResponse = new UserLoginResponse
        {
            Id = userDto.Id,
            Token = _tokenService.GenerateToken(userDto.Password, userDto.Email)
        };
        _logger.Debug($"Successfully logged in with token {userLoginResponse.Token}");

        return await Task.FromResult(Ok(userLoginResponse));
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequest userRegisterRequest, CancellationToken cancellationToken)
    {
        EnsureModelStateIsValid();
        var authenticationRegisterCommand = new AuthenticationRegisterCommand(userRegisterRequest.Email,
                                                                              userRegisterRequest.Password,
                                                                              userRegisterRequest.FirstName,
                                                                              userRegisterRequest.LastName);
        var userDto = await _mediator.Send(authenticationRegisterCommand, cancellationToken);
        string url = GetByIdUrl(userDto.Id);

        return Created(url, _mapper.Map<UserResponse>(userDto));
    }
}