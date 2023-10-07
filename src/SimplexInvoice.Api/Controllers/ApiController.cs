using Microsoft.AspNetCore.Mvc;
using SimplexInvoice.Api.Exceptions;

namespace SimplexInvoice.Api.Controllers;
public abstract class ApiController : ControllerBase
{
    protected string GetByIdUrl(Guid id)
    {
        string url = $"UNKNOWN_URL/{id}";
        if (this.Request is not null)
        {
            string path = this.Request.Path.ToString();
            var baseUrl = path.Substring(0, path.LastIndexOf("/"));
            url = $"{this.Request.Scheme}://{this.Request.Host}{baseUrl}/GetById{id}";
        }

        return url;
    }

    protected void EnsureModelStateIsValid()
    {
        if (!ModelState.IsValid)
            throw new ModelStateValidationException("Validaton error.", ModelState);
    }
}