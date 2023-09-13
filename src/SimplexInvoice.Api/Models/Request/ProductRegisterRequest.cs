using System.ComponentModel.DataAnnotations;

namespace SimplexInvoice.Api.Models.Request;

public class ProductRegisterRequest
{
    [Required(ErrorMessage = "Code is required.")]
    [StringLength(20, ErrorMessage = "Code cannot be longer than 20 characters.")]
    public string Code { get; set; } = string.Empty;
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(40, ErrorMessage = "Name cannot be longer than 40 characters.")]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "Description is required.")]
    [StringLength(40, ErrorMessage = "Description cannot be longer than 40 characters.")]
    public string Description { get; set; } = string.Empty;
    [Required(ErrorMessage = "PackageQuantity is required.")]
    [Range(0.0, Double.MaxValue, ErrorMessage = "PackageQuantity must be greater than 0.")]
    public double PackageQuantity { get; set; }
    [Required(ErrorMessage = "Price is required.")]
    public double Price { get; set; }
    [Required(ErrorMessage = "Currency is required.")]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency must be 3 characters long.")]
    public string Currency { get; set; } = string.Empty;
    [Required(ErrorMessage = "ProductTaxRateId is required.")]
    public Guid ProductTaxRateId { get; set; }

}