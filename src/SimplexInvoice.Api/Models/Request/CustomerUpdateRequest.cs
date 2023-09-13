using System.ComponentModel.DataAnnotations;

namespace SimplexInvoice.Api.Models.Request;

public class CustomerUpdateRequest
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "FirstName is required.")]
    [StringLength(40, ErrorMessage = "FirstName cannot be longer than 40 characters.")]
    public string FirstName { get; set; } = string.Empty;
    [Required(ErrorMessage = "LastName is required.")]
    [StringLength(40, ErrorMessage = "LastName cannot be longer than 40 characters.")]
    public string LastName { get; set; } = string.Empty;
    [Required(ErrorMessage = "IdDocumentTypeId is required.")]
    public Guid IdDocumentTypeId { get; set; }
    [Required(ErrorMessage = "IdDocumentNumber is required.")]
    [StringLength(40, ErrorMessage = "IdDocumentNumber cannot be longer than 40 characters.")]
    public string IdDocumentNumber { get; set; } = string.Empty;
    [Required(ErrorMessage = "Street is required.")]
    [StringLength(40, ErrorMessage = "Street cannot be longer than 40 characters.")]
    public string Street { get; set; } = string.Empty;
    [Required(ErrorMessage = "City is required.")]
    [StringLength(40, ErrorMessage = "City cannot be longer than 40 characters.")]
    public string City { get; set; } = string.Empty;
    [Required(ErrorMessage = "State is required.")]
    [StringLength(40, ErrorMessage = "State cannot be longer than 40 characters.")]
    public string State { get; set; } = string.Empty;
    [Required(ErrorMessage = "Country is required.")]
    [StringLength(40, ErrorMessage = "Country cannot be longer than 40 characters.")]
    public string Country { get; set; } = string.Empty;
    [Required(ErrorMessage = "PostalCode is required.")]
    [StringLength(40, ErrorMessage = "PostalCode cannot be longer than 40 characters.")]
    public string PostalCode { get; set; } = string.Empty;
    [Required(ErrorMessage = "Phone is required.")]
    public string Phone { get; set; } = string.Empty;
    [Required(ErrorMessage = "Email is required.")]
    [StringLength(40, ErrorMessage = "Email cannot be longer than 40 characters.")]
    public string Email { get; set; } = string.Empty;
}