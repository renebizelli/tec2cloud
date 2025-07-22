namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
public class CreateProductResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
    public CreateProductRatingResponse? Rating { get; set; }
}

public class CreateProductRatingResponse
{
    public int Count { get; set; }
    public decimal Rate { get; set; }
}