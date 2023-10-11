using MediatR;
using SimplexInvoice.Application.Common.Dto;

namespace SimplexInvoice.Application.Companies.Queries;

public record GetCompanyQuery() : IRequest<CompanyDto>;