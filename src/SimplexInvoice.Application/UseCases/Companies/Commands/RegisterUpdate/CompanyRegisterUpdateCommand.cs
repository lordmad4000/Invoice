using MediatR;
using SimplexInvoice.Application.Common.Dto;
using System;

namespace SimplexInvoice.Application.Companies.Commands;

public record CompanyRegisterUpdateCommand(string Name,
                                           Guid IdDocumentTypeId,
                                           string IdDocumentNumber,
                                           string Street,
                                           string City,
                                           string State,
                                           string Country,
                                           string PostalCode,
                                           string Phone,
                                           string Email) : IRequest<CompanyDto>;