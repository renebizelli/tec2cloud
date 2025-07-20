using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
public class CreateSaleResponse
{
public int Id { get; set; }
public Guid BranchId { get; set; }
public SaleStatus Status { get; set; }
public UserResponse User { get; set; } = new();
public decimal TotalAmount { get; set; }
public DateTime CreatedAt { get; set; }
public ICollection<SaleItemResponse> Items { get; set; } = [];
}

public class UserResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
public class ProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class SaleItemResponse
{
    public int Id { get; set; }
    public ProductResponse Product { get; set; } = new();
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public SaleItemStatus Status { get; set; }
    public decimal TotalPrice { get { return (Price * Quantity) - Discount; } }
}