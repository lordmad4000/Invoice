using System;
using System.IO;
using System.Threading.Tasks;
using Users.Application.Interfaces;
using Users.Application.Models.ViewModels;

namespace Users.Application.Services
{
    public class NotificationHTMLService : INotificationService
    {
        public async Task<bool> SendAsync(UserViewModel userVM, string activationCode)
        {
            try
            {
                string[] lines =
                {
                    "<!DOCTYPE html>",
                    "<html>",
                    "<body>",
                    "<h1></h1>",
                    $"<p>Click <a href=\"https://localhost:5001/api/User/ActivateUser?activationCode={activationCode}\">here</a> to activate your account.</p>",
                    "</body>",
                    "</html>",
                };
                await File.WriteAllLinesAsync("notification.html", lines);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }
}