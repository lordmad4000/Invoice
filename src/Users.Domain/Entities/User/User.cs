using System;
using Users.Domain.Base;
using Users.Domain.ValueObjects;

namespace Users.Domain.Entities
{
    public partial class User : BaseEntity<Guid>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        
    }
}