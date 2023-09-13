using MediatR;
using SimplexInvoice.Application.Common.Dto;
using System;

namespace SimplexInvoice.Application.Customers.Commands;

public record CustomerUpdateCommand(Guid Id,
                                    string FirstName,
                                    string LastName,
                                    Guid IdDocumentTypeId,
                                    string IdDocumentNumber,
                                    string Street,
                                    string City,
                                    string State,
                                    string Country,
                                    string PostalCode,
                                    string Phone,
                                    string Email) : IRequest<CustomerDto>;