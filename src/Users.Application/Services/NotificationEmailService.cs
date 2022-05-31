using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Users.Application.Interfaces;
using Users.Application.Models;

namespace Users.Application.Services
{
    public class NotificationEmailService : INotificationService
    {
        private IConfiguration _configuration;

        public NotificationEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendAsync(UserDto userDto, string activationCode)
        {
            try
            {
                string fromEmail = _configuration.GetValue<string>("EmailConfig:FromEmail");
                string fromHost = _configuration.GetValue<string>("EmailConfig:FromHost");
                string fromPassword = _configuration.GetValue<string>("EmailConfig:FromPassword");
                using (MailMessage mm = new MailMessage(fromEmail, userDto.Email))
                {
                    mm.Subject = "UsersWebAPI activation";
                    mm.Body = new String(
                         @$"<!DOCTYPE html>
                        <html>
                        <body>
                        <h1></h1>
                        <p>Click <a href=\""https://localhost:5001/api/User/ActivateUser?activationCode={activationCode}\"">here</a> to activate your account.</p>
                        </body>
                        </html>");
                    mm.IsBodyHtml = true;
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = fromHost;
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential(fromEmail, fromPassword);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        await smtp.SendMailAsync(mm);
                    }
                }

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