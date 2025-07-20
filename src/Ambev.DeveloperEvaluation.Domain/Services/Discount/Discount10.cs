using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services.Discount;

public class Discount10 : IDiscount
{
    public void Calculate(SaleItem item)
    {
        item.Discount = item.Price * 0.1M;
    }
}
