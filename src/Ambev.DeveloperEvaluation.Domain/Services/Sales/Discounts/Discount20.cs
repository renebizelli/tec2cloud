using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services.Sales.Discounts;

public class Discount20 : IDiscount
{
    public void Calculate(SaleItem item)
    {
        item.Discount = item.Price * 0.2M;
    }
}
