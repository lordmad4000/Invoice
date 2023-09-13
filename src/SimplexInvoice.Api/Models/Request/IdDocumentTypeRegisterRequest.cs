using System;
using System.ComponentModel.DataAnnotations;

namespace SimplexInvoice.Api.Models.Request;

public class IdDocumentTypeRegisterRequest
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(20, ErrorMessage = "Name cannot be longer than 20 characters.")]
    public string Name { get; set; } = string.Empty;
}