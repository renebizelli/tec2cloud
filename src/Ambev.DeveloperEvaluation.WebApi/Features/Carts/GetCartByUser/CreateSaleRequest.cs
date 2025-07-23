namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartByUser;

public class GetCartByUserRequest
{
    public Guid UserId { get; set; }
    public Guid BranchId { get; set; }
}
