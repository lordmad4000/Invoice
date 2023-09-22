using Microsoft.AspNetCore.Mvc;

namespace SimplexInvoice.Api.Tests.IntegrationTests.Common
{
    internal static class CustomConvert
    {
        public static T? CreatedResultTo<T>(IActionResult actionResult)
        {
            var createdResult = actionResult as CreatedResult;
            if (createdResult is null || createdResult.Value is null)
                return default(T);

            return (T)createdResult.Value;
        }

        public static T? OkResultTo<T>(IActionResult actionResult)
        {            
            var okResult = actionResult as OkObjectResult;
            if (okResult is null || okResult.Value is null)
                return default(T);

            return (T)okResult.Value;
        }

    }
}
