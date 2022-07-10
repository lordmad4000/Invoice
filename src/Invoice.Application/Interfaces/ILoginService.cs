using Invoice.Application.Models;
using System.Threading.Tasks;

namespace Invoice.Application.Interfaces
{
    public interface ILoginService
    {
        Task<UserDto> Login(string email, string password);
    }
}