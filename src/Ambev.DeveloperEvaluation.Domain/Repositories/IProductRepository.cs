using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IProductRepository
{
    Task<Product?> Get(int productId, CancellationToken cancellationToken = default);
    Task<IList<Product>> GetProducts(CancellationToken cancellationToken = default);
}
