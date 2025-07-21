using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Specifications;

namespace Ambev.DeveloperEvaluation.Domain.Services.Sales.Discounts;

public class SaleDiscountApplier : ISaleDiscountApplier
{
    public async Task Applier(Sale sale, CancellationToken cancellationToken = default)
    {
        if (!AllowedDiscount(sale)) return;

        foreach (var item in sale.ActiveItems())
        {
            var discount = DiscountFactory.Get(item.Quantity);
            discount.Calculate(item);
        }
    }

    private bool AllowedDiscount(Sale sale)
    {
        var spec = new AllowedDiscountSpecification();

        return spec.IsSatisfiedBy(sale);
    }
}
