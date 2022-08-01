using System;
using Invoice.Domain.Base;

namespace Invoice.Domain.Entities
{
    public partial class IdDocumentType : IAggregateRoot
    {
        private IdDocumentType()
        {
        }

        public IdDocumentType(string name)
        {
            Id = Guid.NewGuid();
            Update(name);
        }

        public void Update(string name)
        {
            Name = name;
        }

    }
}