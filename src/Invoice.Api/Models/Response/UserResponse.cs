using System;

namespace Invoice.Api.Models.Response
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = String.Empty;
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
    }
}