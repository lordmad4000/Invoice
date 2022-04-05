using System;
using Users.Domain.Base;
using Users.Domain.ValueObjects;

namespace Users.Domain.User
{
    public partial class User : BaseEntity<Guid>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EmailAddress EmailAddress { get; set; }
        
    }
}