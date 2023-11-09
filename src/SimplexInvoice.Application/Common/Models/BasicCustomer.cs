using System;

namespace SimplexInvoice.Application.Common.Models;
public class BasicCustomer
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string IdDocumentNumber { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
}