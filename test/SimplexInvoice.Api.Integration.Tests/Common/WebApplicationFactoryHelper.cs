using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace SimplexInvoice.Api.Integration.Tests.Common
{
    public static class WebApplicationFactoryHelper
    {
        public static HttpClient GetHttpClient(WebApplicationFactory<Program> applicationFactory)
        {            
            return applicationFactory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                });
            }).CreateClient();
        }

    }
}
