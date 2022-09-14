using Invoice.Application.Common.Dto;
using MediatR;

namespace Invoice.Application.CQRS.Authentication.Commands
{
    public class AuthenticationRegisterCommand : IRequest<UserDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public AuthenticationRegisterCommand(string email, string password, string firstName, string lastName)
        {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}