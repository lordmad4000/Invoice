using AutoMapper;
using Invoice.Application.CQRS.Configuration.Commands.Register;
using Invoice.Application.Common.Interfaces.Persistance;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace Invoice.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ICustomLogger _logger;

        public ConfigurationController(IMediator mediator,
                                       IMapper mapper,
                                       ICustomLogger logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
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
    }
}
