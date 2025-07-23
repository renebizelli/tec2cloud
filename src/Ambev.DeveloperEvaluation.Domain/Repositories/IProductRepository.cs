using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IProductRepository
{
    Task<Product> CreateAsync(Product product, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int productId, CancellationToken cancellationToken);
    Task UpdateAsync(Product product, CancellationToken cancellationToken);
    Task<Product?> GetAsync(int productId, CancellationToken cancellationToken);
    Task<(int, IList<Product>)> ListProductsAsync(IProductQueryOptions queryOptions, CancellationToken cancellationToken);
}
