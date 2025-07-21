using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public class AllowedDiscountSpecification : ISpecification<Sale>
{
    public bool IsSatisfiedBy(Sale sale)
    {
        return sale.Items.Count > 4;
    }
}
