using Invoice.Application.CQRS.Users.Common;

namespace Invoice.Application.CQRS.Users.Commands
{
    public class UserRegisterCommand : UserRequest
    {
        public UserRegisterCommand(string email, string password, string firstName, string lastName)
        {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}