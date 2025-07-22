namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;

public class ListProductsRequest
{
    public string? _Order { get; set; }
    public string? Category { get; set; }
    public string? Title { get; set; }
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}
