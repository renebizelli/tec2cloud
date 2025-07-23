namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductResult 
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
    public CreateProductRatingResult? Rating { get; set; }
}

public class CreateProductRatingResult
{
    public int Count { get; set; }
    public decimal Rate { get; set; }
}