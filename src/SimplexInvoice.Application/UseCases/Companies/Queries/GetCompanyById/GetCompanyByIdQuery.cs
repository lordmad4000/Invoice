using MediatR;
using SimplexInvoice.Application.Common.Dto;
using System;

namespace SimplexInvoice.Application.Companies.Queries;

public record GetCompanyByIdQuery(Guid Id) : IRequest<CompanyDto>;