using System;
using Invoice.Domain.Base;
using Invoice.Domain.ValueObjects;

namespace Invoice.Domain.Entities
{
    public partial class User : IAggregateRoot
    {
        private User()
        {
        }

        public User(EmailAddress emailAddress, string password, string firstName, string lastName)
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
            Update(emailAddress, password, firstName, lastName);
        }

        public void Update(EmailAddress emailAddress, string password, string firstName, string lastName)
        {
            EmailAddress = emailAddress;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            return @$"Id {Id}, 
                      Email {EmailAddress.Address}, 
                      Password {Password}, 
                      EncryptedPassword {Password}, 
                      FirstName {FirstName}, 
                      LastName {LastName}";
        }

    }
}