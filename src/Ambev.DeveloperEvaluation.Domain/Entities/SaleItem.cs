using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Services.Sales.Discounts;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem
{

    public SaleItem() // automapper need it;
    {
            
    }
    public SaleItem(int id, Product? product, int quantity, decimal price, decimal discount, int saleId)
    {
        Id = id;
        Product = product;
        Quantity = quantity;
        Price = price;
        Discount = discount;
        SaleId = saleId;
        Status = SaleItemStatus.Active;
    }

    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public int SaleId { get; set; }
    public Sale? Sale { get; set; }
    public SaleItemStatus Status { get; set; }
    public decimal TotalPrice { get { return TotalPriceCalculate(); } }

    private decimal TotalPriceCalculate()=>
        (Price * Quantity) - Discount;

    public void ApplyDiscount(decimal discount)
    {
        Discount = discount;
    }

    public void ApplyPrice(decimal price)
    {
        Price = price;
    }

    public void Cancel()
    {
        Status = SaleItemStatus.Cancelled;
    }
}