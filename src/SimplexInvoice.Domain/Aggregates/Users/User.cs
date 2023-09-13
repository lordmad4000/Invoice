using SimplexInvoice.Domain.ValueObjects;
using System;

namespace SimplexInvoice.Domain.Users
{
    public partial class User
    {
        public EmailAddress EmailAddress { get; private set; } = new EmailAddress("defaultuser@user.com");
        public string Password { get; private set; } = string.Empty;
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public DateTime CreationDate { get; private set; }
    }
}