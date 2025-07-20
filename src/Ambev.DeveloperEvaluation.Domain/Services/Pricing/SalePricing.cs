using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services.Discount;

namespace Ambev.DeveloperEvaluation.Domain.Services.Pricing;

public class SalePricing : ISalePricing
{
    private readonly IProductRepository _productRepository;

    public SalePricing(IProductRepository product)
    {
        _productRepository = product;
    }

    public async Task Pricing(Sale sale, CancellationToken cancellationToken = default)
    {
        foreach (var item in sale.ActiveItems())
        {
            var product = await _productRepository.Get(item.ProductId, cancellationToken);

            if (product == null) throw new InvalidOperationException($"Invalid product id {item.ProductId}");

            item.Price = product.Price;

            var discount = DiscountFactory.Get(item.Quantity);

            discount.Calculate(item);
        }

        sale.TotalAmountCalculate();
    }
}
