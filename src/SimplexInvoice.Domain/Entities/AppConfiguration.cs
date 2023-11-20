using System;

namespace SimplexInvoice.Domain.Entities
{
    public class AppConfiguration
    {
        public Guid Id { get; set; } = Guid.Parse("93cb9570-7f16-4c26-aafc-3b96b2bba055");
        public string LastInvoiceNumber { get; set; } = "0000000000";
    }
}
