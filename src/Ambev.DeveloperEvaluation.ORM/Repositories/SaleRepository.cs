using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Sale?> Get(int saleId, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Include(i=> i.Items.Where(w => w.Status == SaleItemStatus.Active))
                .ThenInclude(i=> i.Product)
            .Include(i => i.User)
            .Include(i => i.Branch)
            .AsNoTracking()
            .Where(w => w.Id.Equals(saleId))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task CreateSale(Sale sale, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync();
    }
}
