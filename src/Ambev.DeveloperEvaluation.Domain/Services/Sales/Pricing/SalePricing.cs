using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Domain.Services.Sales.Pricing;

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
            var product = await _productRepository.GetAsync(item.ProductId, cancellationToken);

            if (product == null) throw new InvalidOperationException($"Invalid product id {item.ProductId}");

            item.ApplyPrice(product.Price);

        }

        sale.TotalAmountCalculate();
    }
}
