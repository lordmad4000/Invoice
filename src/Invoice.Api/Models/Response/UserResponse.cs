using System;

namespace Invoice.Api.Models.Response
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }        
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}