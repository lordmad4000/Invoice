using System;
using System.ComponentModel.DataAnnotations;

namespace SimplexInvoice.Application.Common.Dto;

public partial class IdDocumentTypeDto
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(20, ErrorMessage = "Name cannot be longer than 20 characters.")]
    public string Name { get; set; }
}