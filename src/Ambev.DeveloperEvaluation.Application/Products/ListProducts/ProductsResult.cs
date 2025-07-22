namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

public class ProductsResult
{
    public int TotalCount { get; set; }
    public List<ProductResult> Products { get; set; } = [];
}

public class ProductResult
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
}
