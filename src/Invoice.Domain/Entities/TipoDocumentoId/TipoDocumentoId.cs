using Invoice.Domain.Base;

namespace Invoice.Domain.Entities
{
    public partial class TipoDocumentoId : BaseEntity
    {
        public string Nombre { get; private set; }
    }
}