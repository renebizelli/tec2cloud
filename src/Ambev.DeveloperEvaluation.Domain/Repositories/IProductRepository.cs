using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IProductRepository
{
    Task<IList<Product>> GetProducts(CancellationToken cancellationToken = default);
}
