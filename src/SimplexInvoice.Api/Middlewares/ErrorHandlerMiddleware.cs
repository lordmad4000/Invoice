using SimplexInvoice.Api.Exceptions;
using SimplexInvoice.Api.Models.Response;
using SimplexInvoice.Application.Common.Exceptions;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Infra.Exceptions;
using System.Net;

namespace SimplexInvoice.Api.Middlewares
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
            catch (Exception exception)
            {
                var response = context.Response;
                var bodyError = new HttpResponseBodyError();
                response.ContentType = "application/json";
                _logger.Error($"{exception.GetType().Name} {exception?.Message}");                

                switch (exception)
                {
                    case DataBaseException:
                    {
                        response.StatusCode = 500;
                        break;
                    }
                    case NotFoundException:
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    }
                    case ModelStateValidationException ex:
                    {
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        bodyError.Errors.AddRange(ex.ErrorMessages);
                        break;
                    }
                    default:
                    {
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    }
                }
                bodyError.Message = exception?.Message ?? "Undocumented error";
                bodyError.Code = response.StatusCode;
                await response.WriteAsJsonAsync(bodyError);
            }
        }
    }
}
