using System;

namespace SimplexInvoice.Domain.Entities
{
    public class AppConfiguration
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string LastInvoiceNumber { get; set; } = "0000000000";
    }
}
