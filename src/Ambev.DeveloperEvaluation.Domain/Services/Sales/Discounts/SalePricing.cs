using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Specifications;

namespace Ambev.DeveloperEvaluation.Domain.Services.Sales.Discounts;

public class SalePricing : ISalePricing
{
    public void Applier(Sale sale)
    {
        if (AllowedDiscount(sale))
        {
            foreach (var item in sale.ActiveItems())
            {
                var discount = DiscountFactory.Get(item.Quantity);
                discount.Apply(item);
            }
        }

        sale.TotalAmountCalculate();
    }

    private bool AllowedDiscount(Sale sale)
    {
        var spec = new AllowedDiscountSpecification();

        return spec.IsSatisfiedBy(sale);
    }
}
