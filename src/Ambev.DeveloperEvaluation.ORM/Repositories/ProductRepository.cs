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

    public async Task<Product?> Get(int productId, CancellationToken cancellationToken = default)
    {
        return await _context.Products.FirstOrDefaultAsync(f => f.Id.Equals(productId), cancellationToken);
    }

    public async Task<IList<Product>> GetProducts(CancellationToken cancellationToken = default)
    {
        return await _context.Products.Where(w => w.Active).ToListAsync(cancellationToken);
    }
}
