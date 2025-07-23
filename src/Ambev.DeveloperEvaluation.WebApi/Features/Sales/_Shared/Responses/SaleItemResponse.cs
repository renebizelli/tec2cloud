using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales._Shared.Responses;

public class SaleItemResponse
{
    public int Id { get; set; }
    public ProductResponse Product { get; set; } = new();
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalPrice { get { return Price * Quantity - Discount; } }
}