using Microsoft.EntityFrameworkCore;
using SimplexInvoice.Application.Interfaces;
using SimplexInvoice.Infra.Data;
using System.Threading.Tasks;

namespace SimplexInvoice.Infra.Services;

public class InfraTestService : IInfraTestService
{
    private readonly EFContext _context;

    public InfraTestService(EFContext context)
    {
        _context = context;
    }

    public async Task Test()
    {
        var company = await _context.Company.ToListAsync();
    }

}