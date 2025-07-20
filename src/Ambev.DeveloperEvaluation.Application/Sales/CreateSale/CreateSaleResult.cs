using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleResult
{
    public int Id { get; set; }
    public Guid BranchId { get; set; }
    public SaleStatus Status { get; set; }
    public UserResult User { get; set; } = new();
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<SaleItemResult> Items { get; set; } = [];
}

public class UserResult
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
public class ProductResult
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class SaleItemResult
{
    public int Id { get; set; }
    public ProductResult Product { get; set; } = new();
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public SaleItemStatus Status { get; set; }
    public decimal TotalPrice { get { return (Price * Quantity) - Discount; } }
}