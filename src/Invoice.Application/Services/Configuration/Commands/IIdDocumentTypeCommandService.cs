using System.Threading.Tasks;
using Invoice.Application.Services.Configuration.Common;

namespace Invoice.Application.Services.Configuration.Commands
{
    public interface IIdDocumentTypeCommandService
    {
        Task<IdDocumentTypeResult> Register(string name);
    }
}