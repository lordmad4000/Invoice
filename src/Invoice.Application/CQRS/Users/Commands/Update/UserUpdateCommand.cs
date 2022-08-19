using System;
using Invoice.Application.Common.Dto;
using MediatR;

namespace Invoice.Application.CQRS.Users.Commands.Register
{
    public class UserUpdateCommand : IRequest<UserDto>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

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