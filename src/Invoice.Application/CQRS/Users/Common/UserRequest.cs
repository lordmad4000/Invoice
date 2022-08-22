using System;
using Invoice.Application.Common.Dto;
using MediatR;

namespace Invoice.Application.CQRS.Users.Common
{
    public class UserRequest : IRequest<UserDto>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}