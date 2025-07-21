using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IList<Product>> GetProductsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Products.AsNoTracking().Where(w => w.Active).ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
    }
}
