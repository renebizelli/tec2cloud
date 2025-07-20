using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

internal class DeleteCartHandle : IRequestHandler<DeleteCartCommand>
{
    private readonly ICartRepository _repository;

    public DeleteCartHandle(ICartRepository cartRepository)
    {
        _repository = cartRepository;
    }

    public async Task Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteCart(request.UserId, cancellationToken);
    }
}
