using System;

namespace SimplexInvoice.Domain.Base
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }
    }

}