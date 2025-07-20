using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services.Discount;

public interface IDiscount
{
    void Calculate(SaleItem item);
}
