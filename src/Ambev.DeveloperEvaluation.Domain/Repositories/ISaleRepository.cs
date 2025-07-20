using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository
{
    Task<Sale?> Get(int saleId, Guid userId, CancellationToken cancellationToken = default);
    Task CreateSale(Sale sale, CancellationToken cancellationToken = default);
}
