using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DefaultContext _context;

    public ProductRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<bool> DeleteAsync(int productId, CancellationToken cancellationToken = default)
    {
        var product = await _context.Products.FirstOrDefaultAsync(f => f.Id.Equals(productId) && f.Active, cancellationToken);

        if (product == null)
            return false;

        product!.Active = false;
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<Product?> GetAsync(int productId, CancellationToken cancellationToken = default)
    {
        return await _context.Products.AsNoTracking().FirstOrDefaultAsync(f => f.Id.Equals(productId), cancellationToken);
    }

    public async Task<(int, IList<Product>)> ListProductsAsync(IProductQueryOptions queryOptions, CancellationToken cancellationToken)
    {
        var allowedOrderFields = new Dictionary<string, Expression<Func<Product, object>>>(StringComparer.OrdinalIgnoreCase)
        {
            ["title"] = p => p.Title,
            ["price"] = p => p.Price,
            ["category"] = p => p.Category,
        };

        var query = _context.Products
                    .AsNoTracking()
                    .Where(w => w.Active)
                    .ApplyWhereLike(queryOptions.Category, w => w.Category)
                    .ApplyWhereLike(queryOptions.Title, w => w.Title)
                    .ApplyWhereRange(queryOptions.MinPrice, queryOptions.MaxPrice, w => w.Price)
                    .ApplyOrdering(queryOptions.OrderCriteria, allowedOrderFields);

        var count = await query.CountAsync();

        var products = await query
                        .ApplyPaging(queryOptions.Page, queryOptions.PageSize)
                        .ToListAsync(cancellationToken);

        return (count, products);
    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
    }
}
