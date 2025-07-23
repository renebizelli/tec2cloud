
namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleItemRepository
{
    Task<bool> DeleteAsync(int saleId, int saleItemId, CancellationToken cancellationToken = default);
}
