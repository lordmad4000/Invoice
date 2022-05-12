using System;
using Users.Domain.Base;
using Users.Domain.ValueObjects;

namespace Users.Domain.Entities
{
    public partial class User : IAggregateRoot
    {
        private User()
        {
        }

        public User(string userName, string password, string firstName, string lastName, EmailAddress emailAddress)
        {
            Id = Guid.NewGuid();
            ActivationCode = NewActivationCode();
            Password = password;
            EmailAddress = emailAddress;
            Active = false;
            Update(userName, firstName, lastName);
        }

        public void Activate()
        {
            Active = true;
        }

        public void Update(string userName, string firstName, string lastName)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
        }

        private string NewActivationCode()
        {
            return Guid.NewGuid()
                       .ToString()
                       .Replace("-", "");
        }
    }
}