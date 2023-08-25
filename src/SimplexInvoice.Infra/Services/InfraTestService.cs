using Microsoft.EntityFrameworkCore;
using SimplexInvoice.Application.Interfaces;
using SimplexInvoice.Infra.Data;
using System.Linq;
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
        //await _context.Invoice.ToListAsync();
        var invoice = await _context.Invoice.Include(c => c.InvoiceLines)
                                            .FirstOrDefaultAsync(c => c.Number.Equals("1"));

        if (company is null)
        {
            return;
        }

    }

}