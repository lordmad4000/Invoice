using Invoice.Application.CQRS.Users.Common;
using System;

namespace Invoice.Application.CQRS.Users.Queries
{
    public class GetUserByIdQuery : UserRequest
    {
        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}