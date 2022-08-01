using System;
using Invoice.Domain.Base;

namespace Invoice.Domain.Entities
{
    public partial class TipoDocumentoId : IAggregateRoot
    {
        private TipoDocumentoId()
        {
        }

        public TipoDocumentoId(string nombre)
        {
            Id = Guid.NewGuid();
            Update(nombre);
        }

        public void Update(string nombre)
        {
            Nombre = nombre;
        }

    }
}