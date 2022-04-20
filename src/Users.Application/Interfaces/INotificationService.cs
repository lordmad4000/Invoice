using System.Threading.Tasks;
using Users.Application.Services;

namespace Users.Application.Interfaces
{
    public interface INotificationService
    {
        Task<bool> SendAsync(UserViewModel userVM, string activationCode);
    }
}