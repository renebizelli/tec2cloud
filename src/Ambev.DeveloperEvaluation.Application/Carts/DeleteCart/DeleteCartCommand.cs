using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

public class DeleteCartCommand : IRequest
{
    public Guid UserId { get; set; }
    public Guid BranchId { get; set; }
}
