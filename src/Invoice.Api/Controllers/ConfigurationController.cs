using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Invoice.Domain.Exceptions;
using Invoice.Infra.Exceptions;
using Invoice.Application.Services.Configuration.Commands;

namespace Invoice.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IIdDocumentTypeCommandService _idDocumentTypeCommandService;
        private readonly IMapper _mapper;

        public ConfigurationController(IIdDocumentTypeCommandService idDocumentTypeCommandService,
                                       IMapper mapper)
        {
            _idDocumentTypeCommandService = idDocumentTypeCommandService;
            _mapper = mapper;
        }

        [HttpPost("IdDocumentTypeRegister")]
        public async Task<IActionResult> IdDocumentTypeRegister(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                    return BadRequest("Name is required");

                var idDocumentTypeResult = await _idDocumentTypeCommandService.Register(name);
                var url = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/{idDocumentTypeResult.Id}";

                return (Created(url, idDocumentTypeResult));
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
