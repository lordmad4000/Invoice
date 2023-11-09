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

[ApiController]
[Route("api/[controller]")]
public class TestsController : ApiController
{
    private readonly ICustomLogger _logger;
    public TestsController(ICustomLogger logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        await Task.Delay(500);
        Console.WriteLine("Hello World.");
        return Ok("Hello World,");
    }

}
