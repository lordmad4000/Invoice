using System.Threading.Tasks;
using SimplexInvoice.Application.Interfaces;

namespace SimplexInvoice.Application.Services
{

    public class ApplicationTestService
    {
        private readonly IInfraTestService _infraTestService;

        public ApplicationTestService(IInfraTestService infraTestService)
        {
            _infraTestService = infraTestService;
        }

        public async Task Test()
        {
            await _infraTestService.Test();
        }

    }
}