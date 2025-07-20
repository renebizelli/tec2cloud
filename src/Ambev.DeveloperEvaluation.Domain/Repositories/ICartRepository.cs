using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ICartRepository
{
    Task<Cart?> GetCartByUser(CartFilter cartFilter, CancellationToken cancellationToken = default);
    Task CreateOrUpdateCart(Cart cart, CancellationToken cancellationToken = default);
    Task DeleteCart(CartFilter cartFilter, CancellationToken cancellationToken = default);
}
