using System.Threading.Tasks;
using Users.Application.Models;
using Users.Application.Services;

namespace Users.Application.Interfaces
{
    public interface INotificationService
    {
        Task<bool> SendAsync(UserDto userVM, string activationCode);
    }
}