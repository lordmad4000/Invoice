using Invoice.Domain.Base;

namespace Invoice.Domain.Entities
{
    public partial class IdDocumentType : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
    }
}