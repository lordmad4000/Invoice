using System;

namespace Users.API.Models.Response
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }        
    }
}