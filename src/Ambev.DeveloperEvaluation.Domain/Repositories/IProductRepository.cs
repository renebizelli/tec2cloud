using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IProductRepository
{
    Task<Product> CreateAsync(Product product, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int productId, CancellationToken cancellationToken);
    Task UpdateAsync(Product product, CancellationToken cancellationToken);
    Task<Product?> GetAsync(int productId, CancellationToken cancellationToken);
    Task<IList<Product>> GetProductsAsync(CancellationToken cancellationToken);
}
