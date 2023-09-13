using System.ComponentModel.DataAnnotations;

namespace SimplexInvoice.Api.Models.Request;

public class InvoiceLineRegisterRequest
{
    [Required(ErrorMessage = "LineNumber is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "LineNumber must be greater than 0.")]
    public int LineNumber { get; set; }
    [Required(ErrorMessage = "ProductCode is required.")]
    [StringLength(20, ErrorMessage = "ProductCode cannot be longer than 20 characters.")]
    public string ProductCode { get; set; } = string.Empty;
    [Required(ErrorMessage = "ProductName is required.")]
    [StringLength(40, ErrorMessage = "ProductName cannot be longer than 40 characters.")]
    public string ProductName { get; set; } = string.Empty;
    [Required(ErrorMessage = "ProductDescription is required.")]
    [StringLength(40, ErrorMessage = "ProductDescription cannot be longer than 40 characters.")]
    public string ProductDescription { get; set; } = string.Empty;
    [Required(ErrorMessage = "Packages is required.")]
    public int Packages { get; set; }
    [Required(ErrorMessage = "Quantity is required.")]
    public double Quantity { get; set; }
    [Required(ErrorMessage = "Price is required.")]
    public double Price { get; set; }
    [Required(ErrorMessage = "Currency is required.")]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency must be 3 characters long.")]
    public string Currency { get; set; } = string.Empty;
    [Required(ErrorMessage = "TaxName is required.")]
    [StringLength(20, ErrorMessage = "TaxName cannot be longer than 20 characters.")]
    public string TaxName { get; set; } = string.Empty;
    [Required(ErrorMessage = "TaxRate is required.")]
    [Range(0, 100, ErrorMessage = "TaxRate must be between 0 and 100.")]
    public int TaxRate { get; set; }
    [Required(ErrorMessage = "DiscountRate is required.")]
    [Range(0, 100, ErrorMessage = "DiscountRate must be between 0 and 100.")]
    public int DiscountRate { get; set; }
}