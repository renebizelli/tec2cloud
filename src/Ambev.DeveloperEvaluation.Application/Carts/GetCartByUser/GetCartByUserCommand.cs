using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCartByUser;

public class GetCartByUserCommand : IRequest<GetCartByUserResult>
{
    public Guid UserId { get; set; }
}
