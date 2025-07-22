namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;

public class ProductsResponse
{
    public int TotalCount { get; set; }
    public List<ProductResponse> Products { get; set; } = [];
}

public class ProductResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
}
