using Invoice.Application.CQRS.Users.Common;
using System;

namespace Invoice.Application.CQRS.Users.Commands
{
    public class UserUpdateCommand : UserRequest
    {
        public UserUpdateCommand(Guid id, string email, string password, string firstName, string lastName)
        {
            Id = id;
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}