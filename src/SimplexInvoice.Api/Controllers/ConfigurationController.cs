using AutoMapper;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.IdDocumentTypes.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimplexInvoice.Application.Services;

namespace SimplexInvoice.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ICustomLogger _logger;
        private readonly ApplicationTestService _applicationTestService;

        public ConfigurationController(IMediator mediator,
                                       IMapper mapper,
                                       ICustomLogger logger,
                                       ApplicationTestService applicationTestService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
            _applicationTestService = applicationTestService;
        }

        [HttpPost("IdDocumentTypeRegister")]
        public async Task<IActionResult> IdDocumentTypeRegister(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("Name is required");

            var idDocumentTypeDto = await _mediator.Send(new IdDocumentTypeRegisterCommand(name));
            var url = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/{idDocumentTypeDto.Id}";

            return (Created(url, idDocumentTypeDto));
        }

        [HttpGet("Test")]
        [AllowAnonymous]
        public async Task<IActionResult> Test()
        {
            await _applicationTestService.Test();

            return Ok();            
        }
    }
}
