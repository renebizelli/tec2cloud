using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ICartRepository
{
    Task<Cart?> GetCartByUser(Guid userId, CancellationToken cancellationToken = default);
    Task CreateOrUpdateCart(Cart cart, CancellationToken cancellationToken = default);
}
