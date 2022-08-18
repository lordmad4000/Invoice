using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Invoice.Domain.Exceptions;
using Invoice.Infra.Exceptions;
using MediatR;
using Invoice.Application.CQRS.Configuration.Commands.Register;

namespace Invoice.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ConfigurationController(IMediator mediator,
                                       IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("IdDocumentTypeRegister")]
        public async Task<IActionResult> IdDocumentTypeRegister(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                    return BadRequest("Name is required");

                var idDocumentTypeDto = await _mediator.Send(new IdDocumentTypeRegisterCommand(name));

                var url = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/{idDocumentTypeDto.Id}";

                return (Created(url, idDocumentTypeDto));
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
