namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateOrUpdateCart;

public class CreateOrUpdateCartRequest
{
    public ICollection<CartItem> Items { get; set; } = [];

    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
