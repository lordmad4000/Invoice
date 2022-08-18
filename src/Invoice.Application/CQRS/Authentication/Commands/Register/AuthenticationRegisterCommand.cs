using Invoice.Application.Common.Dto;
using MediatR;

namespace Invoice.Application.CQRS.Authentication.Commands.Register
{
    public class AuthenticationRegisterCommand : IRequest<UserDto>
    {
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public AuthenticationRegisterCommand(string email, string password, string firstName, string lastName)
        {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}