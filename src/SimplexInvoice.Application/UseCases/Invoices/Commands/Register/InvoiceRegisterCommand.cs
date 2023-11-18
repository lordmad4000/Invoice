using SimplexInvoice.Application.Common.Dto;
using MediatR;
using System.Collections.Generic;
using System;

namespace SimplexInvoice.Application.Invoices.Commands;

public record InvoiceRegisterCommand(string Description,
                                     DateOnly Date,
                                     string CompanyName,
                                     string CompanyIdDocumentType,
                                     string CompanyDocumentNumber,
                                     string CompanyStreet,
                                     string CompanyCity,
                                     string CompanyState,
                                     string CompanyCountry,
                                     string CompanyPostalCode,
                                     string CompanyPhone,
                                     string CompanyEmail,
                                     string CustomerFullName,
                                     string CustomerIdDocumentType,
                                     string CustomerDocumentNumber,
                                     string CustomerStreet,
                                     string CustomerCity,
                                     string CustomerState,
                                     string CustomerCountry,
                                     string CustomerPostalCode,
                                     string CustomerPhone,
                                     string CustomerEmail,
                                     ICollection<InvoiceLineDto> InvoiceLines) : IRequest<InvoiceDto>;