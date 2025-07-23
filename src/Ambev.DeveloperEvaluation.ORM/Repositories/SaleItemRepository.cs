using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleItemRepository : ISaleItemRepository
{
    private readonly DefaultContext _context;

    public SaleItemRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<bool> DeleteAsync(int saleId, int saleItemId, CancellationToken cancellationToken = default)
    {
        var saleItem = await _context.SaleItems.FirstOrDefaultAsync(f => f.Id.Equals(saleItemId) && f.SaleId.Equals(saleId) && f.Status == SaleItemStatus.Active, cancellationToken);

        if (saleItem == null)
            return false;

        saleItem!.Status = SaleItemStatus.Cancelled;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
