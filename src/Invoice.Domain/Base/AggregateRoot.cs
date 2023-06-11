using System;

namespace Invoice.Domain.Base
{
    public abstract class AggregateRoot : Entity
    {
        protected AggregateRoot(Guid id) : base(id)
        {
        }
    }
}