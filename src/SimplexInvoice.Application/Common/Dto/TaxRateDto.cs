using System;
using System.ComponentModel.DataAnnotations;

namespace SimplexInvoice.Application.Common.Dto;

public class TaxRateDto
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(20, ErrorMessage = "Name cannot be longer than 20 characters.")]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "Value is required.")]
    [Range(0, 100, ErrorMessage = "Value must be between 0 and 100.")]
    public int Value { get; set; } = 0;
}