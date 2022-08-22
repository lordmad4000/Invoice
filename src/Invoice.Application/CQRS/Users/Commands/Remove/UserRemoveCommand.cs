using Invoice.Application.CQRS.Users.Common;
using System;

namespace Invoice.Application.CQRS.Users.Commands
{
    public class UserRemoveCommand : UserRequest
    {
        public UserRemoveCommand(Guid id)
        {
            Id = id;
        }
    }
}