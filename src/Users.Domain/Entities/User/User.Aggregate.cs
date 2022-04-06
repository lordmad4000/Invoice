using Users.Domain.Base;
using Users.Domain.ValueObjects;

namespace Users.Domain.Entities
{
    public partial class User : IAggregateRoot
    {
        public User(string userName, string password, string firstName, string lastName, string emailAddress)
        {
            Update(userName, password, firstName, lastName, emailAddress);
        }

        public void Update(string userName, string password, string firstName, string lastName, string emailAddress)
        {
            UserName = userName;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
        }
    }
}