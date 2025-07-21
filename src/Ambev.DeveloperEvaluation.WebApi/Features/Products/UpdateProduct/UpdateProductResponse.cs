namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
public class UpdateProductResponse
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
    public UpdateProductRatingResponse? Rating { get; set; }
}

public class UpdateProductRatingResponse
{
    public int Count { get; set; }
    public decimal Rate { get; set; }
}