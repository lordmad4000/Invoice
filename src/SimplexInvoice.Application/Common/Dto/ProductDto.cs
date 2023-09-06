using System;
using System.ComponentModel.DataAnnotations;

namespace SimplexInvoice.Application.Common.Dto
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Code is required.")]
        [StringLength(20, ErrorMessage = "Code cannot be longer than 20 characters.")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(40, ErrorMessage = "Name cannot be longer than 40 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(40, ErrorMessage = "Description cannot be longer than 40 characters.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "PackageQuantity is required.")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "PackageQuantity must be greater than 0.")]
        public double PackageQuantity { get; set; }
        [Required(ErrorMessage = "Price is required.")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Currency is required.")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency must be 3 characters long.")]
        public string Currency { get; set; }
        [Required(ErrorMessage = "ProductTaxRateId is required.")]
        public Guid ProductTaxRateId { get; set; }
    }
}