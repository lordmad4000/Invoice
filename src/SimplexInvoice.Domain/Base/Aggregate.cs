using System;

namespace SimplexInvoice.Domain.Base
{
    public abstract class Aggregate : Entity
    {
        protected Aggregate(Guid id) : base(id)
        {
        }
    }
}