using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository
{
    Task<Sale?> GetAsync(int saleId, CancellationToken cancellationToken = default);
    Task<Sale?> GetAttachedAsync(int saleId, CancellationToken cancellationToken = default);
    Task CreateSaleAsync(Sale sale, CancellationToken cancellationToken = default);
    Task<(int, IList<Sale>)> ListSalesAsync(ISalesQueryOptions queryOptions, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int saleId, CancellationToken cancellationToken = default);
    Task UpdateAsync(Sale sale, CancellationToken cancellationToken);
}
