using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales._Shared.Results;

public class SaleItemResult
{
    public int Id { get; set; }
    public ProductResult Product { get; set; } = new();
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public SaleItemStatus Status { get; set; }
    public decimal TotalPrice { get { return Price * Quantity - Discount; } }
}