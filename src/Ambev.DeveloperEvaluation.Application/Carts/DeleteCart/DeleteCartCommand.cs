using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

internal class DeleteCartCommand : IRequest
{
    public Guid UserId { get; set; }
}
