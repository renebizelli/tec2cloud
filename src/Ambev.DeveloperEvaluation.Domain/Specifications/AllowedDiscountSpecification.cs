using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public class AllowedDiscountSpecification : ISpecification<Sale>
{
    public bool IsSatisfiedBy(Sale sale)
    {
        return sale.Items.Count(w => w.Status == SaleItemStatus.Active) > 4;
    }
}
