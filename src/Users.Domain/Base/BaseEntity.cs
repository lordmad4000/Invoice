using System;

namespace Users.Domain.Base
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }
    }

}