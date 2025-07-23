using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Extensions;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Sale?> GetAttachedAsync(int saleId, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Include(i => i.Items)
            .Where(w => w.Id.Equals(saleId))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Sale?> GetAsync(int saleId, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Include(i => i.Items.Where(w => w.Status == SaleItemStatus.Active && w.Product != null).OrderBy(o => o.Product!.Title))
                .ThenInclude(i => i.Product)
            .Include(i => i.User)
            .Include(i => i.Branch)
            .AsNoTracking()
            .Where(w => w.Id.Equals(saleId))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task CreateSaleAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync();
    }

    public async Task<(int, IList<Sale>)> ListSalesAsync(ISalesQueryOptions queryOptions, CancellationToken cancellationToken)
    {
        var allowedOrderFields = new Dictionary<string, Expression<Func<Sale, object>>>(StringComparer.OrdinalIgnoreCase)
        {
            ["TotalAmount"] = p => p.TotalAmount,
            ["User"] = p => p.User!.Username,
            ["CreatedAt"] = p => p.CreatedAt,
            ["Branch"] = p => p.Branch!.Name,
        };

        var query = _context.Sales
                    .AsNoTracking()
                    .Include(i => i.User)
                    .Include(i => i.Branch)
                    .Where(w => w.Status == SaleStatus.Active)
                    .Where(w => queryOptions.BranchId.Equals(Guid.Empty) || w.BranchId.Equals(queryOptions.BranchId))
                    .Where(w => queryOptions.UserId.Equals(Guid.Empty) || w.UserId.Equals(queryOptions.UserId))
                    .ApplyWhereRange(queryOptions.MinTotalPrice, queryOptions.MaxTotalPrice, w => w.TotalAmount)
                    .ApplyWhereRange(queryOptions.MinDate, queryOptions.MaxDate, w => w.CreatedAt)
                    .ApplyOrdering(queryOptions.OrderCriteria, allowedOrderFields);

        var count = await query.CountAsync();

        var products = await query
                        .ApplyPaging(queryOptions.Page, queryOptions.PageSize)
                        .ToListAsync(cancellationToken);

        return (count, products);
    }

    public async Task<bool> DeleteAsync(int saleId, CancellationToken cancellationToken = default)
    {
        var sale = await _context.Sales.FirstOrDefaultAsync(f => f.Id.Equals(saleId) && f.Status == SaleStatus.Active, cancellationToken);

        if (sale == null)
            return false;

        sale.Cancel();

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task UpdateAsync(Sale sale, CancellationToken cancellationToken)
    {
        //_context.Entry(sale).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
    }
}
