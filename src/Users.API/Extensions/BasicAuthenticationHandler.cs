using Microsoft.AspNetCore.Authentication;  
using Microsoft.Extensions.Logging;  
using Microsoft.Extensions.Options;
using System.Linq;  
using System.Net.Http.Headers;  
using System.Security.Claims;  
using System.Text.Encodings.Web;  
using System.Text;  
using System.Threading.Tasks;  
using System;  
using Users.Application.Interfaces;

namespace Users.API.Extensions
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserService _userService;

        public BasicAuthenticationHandler(IUserService userService,
                                          IOptionsMonitor<AuthenticationSchemeOptions> options,
                                          ILoggerFactory logger,
                                          UrlEncoder encoder,
                                          ISystemClock clock) : base(options, logger, encoder, clock)
        {
            _userService = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string username = null;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');
                username = credentials.FirstOrDefault();
                var password = credentials.LastOrDefault();

                var isValid = await _userService.ValidateCredentials(username, password);

                if (!isValid)
                    throw new ArgumentException("Invalid credentials");
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail($"Authentication failed: {ex.Message}");
            }

            var claims = new[] {
                new Claim(ClaimTypes.Name, username)
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}