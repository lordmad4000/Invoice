using SimplexInvoice.Application.Common.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace SimplexInvoice.Api.Models.Request;
public class InvoiceRegisterRequest
{    
    [StringLength(20, ErrorMessage = "Number cannot be longer than 20 characters.")]
    public string Number { get; set; } = string.Empty;
    [Required(ErrorMessage = "Description is required.")]
    [StringLength(40, ErrorMessage = "Description cannot be longer than 40 characters.")]
    public string Description { get; set; } = string.Empty;
    [Required(ErrorMessage = "Date is required.")]
    public DateOnly Date { get; set; }
    public string CorrectionNumber { get; set; } = string.Empty;
    public DateOnly? CorrectionDate { get; set; }
    [Required(ErrorMessage = "CompanyName is required.")]
    [StringLength(40, ErrorMessage = "CompanyName cannot be longer than 40 characters.")]
    public string CompanyName { get; set; } = string.Empty;
    [Required(ErrorMessage = "CompanyIdDocumentType is required.")]
    public string CompanyIdDocumentType { get; set; } = string.Empty;
    [Required(ErrorMessage = "CompanyDocumentNumber is required.")]
    [StringLength(40, ErrorMessage = "CompanyDocumentNumber cannot be longer than 40 characters.")]
    public string CompanyDocumentNumber { get; set; } = string.Empty;
    public string CompanyStreet { get; set; } = string.Empty;
    [Required(ErrorMessage = "CompanyCity is required.")]
    [StringLength(40, ErrorMessage = "CompanyCity cannot be longer than 40 characters.")]
    public string CompanyCity { get; set; } = string.Empty;
    [Required(ErrorMessage = "CompanyState is required.")]
    [StringLength(40, ErrorMessage = "CompanyState cannot be longer than 40 characters.")]
    public string CompanyState { get; set; } = string.Empty;
    [Required(ErrorMessage = "CompanyCountry is required.")]
    [StringLength(40, ErrorMessage = "CompanyCountry cannot be longer than 40 characters.")]
    public string CompanyCountry { get; set; } = string.Empty;
    [Required(ErrorMessage = "CompanyPostalCode is required.")]
    [StringLength(40, ErrorMessage = "CompanyPostalCode cannot be longer than 40 characters.")]
    public string CompanyPostalCode { get; set; } = string.Empty;
    [Required(ErrorMessage = "CompanyPhone is required.")]
    public string CompanyPhone { get; set; } = string.Empty;
    [Required(ErrorMessage = "CompanyEmail is required.")]
    [StringLength(40, ErrorMessage = "CompanyEmail cannot be longer than 40 characters.")]
    public string CompanyEmail { get; set; } = string.Empty;
    [Required(ErrorMessage = "CustomerFullName is required.")]
    [StringLength(81, ErrorMessage = "CustomerFullName cannot be longer than 81 characters.")]
    public string CustomerFullName { get; set; } = string.Empty;
    [Required(ErrorMessage = "CustomerIdDocumentType is required.")]
    public string CustomerIdDocumentType { get; set; } = string.Empty;
    [Required(ErrorMessage = "CustomerDocumentNumber is required.")]
    [StringLength(40, ErrorMessage = "CustomerDocumentNumber cannot be longer than 40 characters.")]
    public string CustomerDocumentNumber { get; set; } = string.Empty;
    public string CustomerStreet { get; set; } = string.Empty;
    [Required(ErrorMessage = "CustomerCity is required.")]
    [StringLength(40, ErrorMessage = "CustomerCity cannot be longer than 40 characters.")]
    public string CustomerCity { get; set; } = string.Empty;
    [Required(ErrorMessage = "CustomerState is required.")]
    [StringLength(40, ErrorMessage = "CustomerState cannot be longer than 40 characters.")]
    public string CustomerState { get; set; } = string.Empty;
    [Required(ErrorMessage = "CustomerCountry is required.")]
    [StringLength(40, ErrorMessage = "CustomerCountry cannot be longer than 40 characters.")]
    public string CustomerCountry { get; set; } = string.Empty;
    [Required(ErrorMessage = "CustomerPostalCode is required.")]
    [StringLength(40, ErrorMessage = "CustomerPostalCode cannot be longer than 40 characters.")]
    public string CustomerPostalCode { get; set; } = string.Empty;
    [Required(ErrorMessage = "CustomerPhone is required.")]
    public string CustomerPhone { get; set; } = string.Empty;
    [Required(ErrorMessage = "CustomerEmail is required.")]
    [StringLength(40, ErrorMessage = "CustomerEmail cannot be longer than 40 characters.")]
    public string CustomerEmail { get; set; } = string.Empty;
    [Required(ErrorMessage = "InvoiceLines is required.")]
    [CollectionBiggerThan<InvoiceLineRegisterRequest>(1, ErrorMessage: "InvoiceLines must be greater than 0.")]
    public List<InvoiceLineRegisterRequest> InvoiceLines { get; set; } = new List<InvoiceLineRegisterRequest>();
}