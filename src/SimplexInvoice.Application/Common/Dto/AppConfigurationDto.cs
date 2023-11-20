using System.ComponentModel.DataAnnotations;

namespace SimplexInvoice.Application.Common.Dto;

public class AppConfigurationDto
{
    [Required(ErrorMessage = "Name is required.")]
    public string LastInvoiceNumber { get; set; }
}