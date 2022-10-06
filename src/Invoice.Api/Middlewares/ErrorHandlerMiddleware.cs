using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Invoice.Application.Common.Interfaces.Persistance;
using Invoice.Infra.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Invoice.Api.Middlewares
{
    public class ErrorHandlerMiddleware                 
    {
        private readonly RequestDelegate _next;
        private readonly ICustomLogger _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ICustomLogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                _logger.Debug($"{error.GetType().Name} {error?.Message}");

                switch (error)
                {
                    case DataBaseException ex:
                    {
                        response.StatusCode = 500;
                        break;
                    }
                    default:
                    {
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    }
                }
                var result = JsonSerializer.Serialize(new { ErrorMessage = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}