using Invoice.Application.Common.Dto;
using MediatR;

namespace Invoice.Application.CQRS.Authentication.Queries
{
    public class LoginQuery : IRequest<UserDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginQuery(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}