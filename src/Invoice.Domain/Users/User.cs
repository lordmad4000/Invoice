using System;
using Invoice.Domain.Base;
using Invoice.Domain.ValueObjects;

namespace Invoice.Domain.Users
{
    public partial class User : BaseEntity
    {
        public EmailAddress EmailAddress { get; private set; }
        public string Password { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime CreationDate { get; private set; }
    }
}