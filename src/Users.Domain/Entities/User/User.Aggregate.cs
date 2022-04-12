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

        public User(Guid id, string userName, string password, string firstName, string lastName, EmailAddress emailAddress)
        {
            Update(id, userName, password, firstName, lastName, emailAddress);
        }

        public void Update(Guid id, string userName, string password, string firstName, string lastName, EmailAddress emailAddress)
        {
            Id = id;
            UserName = userName;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
        }
    }
}