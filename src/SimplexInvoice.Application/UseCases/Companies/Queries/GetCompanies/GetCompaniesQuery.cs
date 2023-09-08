using MediatR;
using SimplexInvoice.Application.Common.Dto;
using System.Collections.Generic;

namespace SimplexInvoice.Application.Companies.Queries;

public record GetCompaniesQuery() :  IRequest<ICollection<CompanyDto>>;