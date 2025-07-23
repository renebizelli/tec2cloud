using Ambev.DeveloperEvaluation.WebApi.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;

public class ListProductsRequest : ListRequest
{
    public string? Category { get; set; }
    public string? Title { get; set; }
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; }

}
