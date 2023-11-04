using SimplexInvoice.Application.Common.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimplexInvoice.Application.Common.Dto;
public class InvoiceDto
{    
    public Guid Id {  get; set; }
    [Required(ErrorMessage = "Number is required.")]
    [StringLength(20, ErrorMessage = "Number cannot be longer than 20 characters.")]
    public string Number { get; set; }
    [Required(ErrorMessage = "Description is required.")]
    [StringLength(40, ErrorMessage = "Description cannot be longer than 40 characters.")]
    public string Description { get; set; }
    [Required(ErrorMessage = "Date is required.")]
    public DateOnly Date { get; set; }
    [Required(ErrorMessage = "CompanyName is required.")]
    [StringLength(40, ErrorMessage = "CompanyName cannot be longer than 40 characters.")]
    public string CompanyName { get; set; }
    [Required(ErrorMessage = "CompanyIdDocumentType is required.")]
    public string CompanyIdDocumentType { get; set; }
    [Required(ErrorMessage = "CompanyDocumentNumber is required.")]
    [StringLength(40, ErrorMessage = "CompanyDocumentNumber cannot be longer than 40 characters.")]
    public string CompanyDocumentNumber { get; set; }
    public string CompanyStreet { get; set; }
    [Required(ErrorMessage = "CompanyCity is required.")]
    [StringLength(40, ErrorMessage = "CompanyCity cannot be longer than 40 characters.")]
    public string CompanyCity { get; set; }
    [Required(ErrorMessage = "CompanyState is required.")]
    [StringLength(40, ErrorMessage = "CompanyState cannot be longer than 40 characters.")]
    public string CompanyState { get; set; }
    [Required(ErrorMessage = "CompanyCountry is required.")]
    [StringLength(40, ErrorMessage = "CompanyCountry cannot be longer than 40 characters.")]
    public string CompanyCountry { get; set; }
    [Required(ErrorMessage = "CompanyPostalCode is required.")]
    [StringLength(40, ErrorMessage = "CompanyPostalCode cannot be longer than 40 characters.")]
    public string CompanyPostalCode { get; set; }
    [Required(ErrorMessage = "CompanyPhone is required.")]
    public string CompanyPhone { get; set; }
    [Required(ErrorMessage = "CompanyEmail is required.")]
    [StringLength(40, ErrorMessage = "CompanyEmail cannot be longer than 40 characters.")]
    public string CompanyEmail { get; set; }
    [Required(ErrorMessage = "CustomerFullName is required.")]
    [StringLength(81, ErrorMessage = "CustomerFullName cannot be longer than 81 characters.")]
    public string CustomerFullName { get; set; }
    [Required(ErrorMessage = "CustomerIdDocumentType is required.")]
    public string CustomerIdDocumentType { get; set; }
    [Required(ErrorMessage = "CustomerDocumentNumber is required.")]
    [StringLength(40, ErrorMessage = "CustomerDocumentNumber cannot be longer than 40 characters.")]
    public string CustomerDocumentNumber { get; set; }
    public string CustomerStreet { get; set; }
    [Required(ErrorMessage = "CustomerCity is required.")]
    [StringLength(40, ErrorMessage = "CustomerCity cannot be longer than 40 characters.")]
    public string CustomerCity { get; set; }
    [Required(ErrorMessage = "CustomerState is required.")]
    [StringLength(40, ErrorMessage = "CustomerState cannot be longer than 40 characters.")]
    public string CustomerState { get; set; }
    [Required(ErrorMessage = "CustomerCountry is required.")]
    [StringLength(40, ErrorMessage = "CustomerCountry cannot be longer than 40 characters.")]
    public string CustomerCountry { get; set; }
    [Required(ErrorMessage = "CustomerPostalCode is required.")]
    [StringLength(40, ErrorMessage = "CustomerPostalCode cannot be longer than 40 characters.")]
    public string CustomerPostalCode { get; set; }
    [Required(ErrorMessage = "CustomerPhone is required.")]
    public string CustomerPhone { get; set; }
    [Required(ErrorMessage = "CustomerEmail is required.")]
    [StringLength(40, ErrorMessage = "CustomerEmail cannot be longer than 40 characters.")]
    public string CustomerEmail { get; set; }
    [Required(ErrorMessage = "TotalTax is required.")]
    public double TotalTax { get; set; }
    [Required(ErrorMessage = "TotalDiscount is required.")]
    public double TotalDiscount { get; set; }
    [Required(ErrorMessage = "TotalTaxBase is required.")]
    public double TotalTaxBase { get; set; }
    [Required(ErrorMessage = "Total is required.")]
    public double Total { get; set; }
    [Required(ErrorMessage = "Currency is required.")]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency must be 3 characters long.")]
    public string Currency { get; set; }
    [Required(ErrorMessage = "InvoiceLines is required.")]
    [CollectionBiggerThan<InvoiceLineDto>(1, ErrorMessage: "InvoiceLines must be greater than 0.")]
    public ICollection<InvoiceLineDto> InvoiceLines { get; set; }
    public ICollection<TotalTaxDto> TotalTaxes { get; set; }
}