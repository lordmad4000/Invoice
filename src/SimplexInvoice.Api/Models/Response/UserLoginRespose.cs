using System;

namespace SimplexInvoice.Api.Models.Response
{
    public class UserLoginResponse
    {
        public Guid Id { get; set; }
        public string Token { get; set; } = String.Empty;
    }
}