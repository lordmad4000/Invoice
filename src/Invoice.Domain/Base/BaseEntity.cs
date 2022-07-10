using System;

namespace Invoice.Domain.Base
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }
    }

}