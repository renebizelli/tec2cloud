using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public class SaleItemQuantitySpecification : ISpecification<SaleItem>
{
    public bool IsSatisfiedBy(SaleItem item)
    {
        return item.Quantity > 0 && item.Quantity <= 20;
    }
}
