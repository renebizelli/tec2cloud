using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services.Sales.Discounts;

public interface IDiscount
{
    void Apply(SaleItem item);
}
