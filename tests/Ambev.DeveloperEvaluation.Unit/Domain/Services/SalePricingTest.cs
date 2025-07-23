using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services.Sales.Pricing;
using Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;
using Ambev.DeveloperEvaluation.Unit.Domain.Services.TestData;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Reflection.Metadata;
using System.Threading;
using Xunit;
using Xunit.Abstractions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Services;

public class SalePricingTest
{
    private readonly SalePricing _salePricing;
    private readonly IProductRepository _productRepository;

    public SalePricingTest()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _salePricing = new SalePricing(_productRepository);
    }

    [Fact(DisplayName = "Given valid product data When apply pricing Then should not have error")]
    public async Task Handle_ValidProduct_ShouldNotHaveError()
    {
        var product = SalePricingTestData.GenerateValidProduct();

        var sale = new Sale();

        var items = new List<SaleItem>
            {
                new SaleItem(0, product, 0, 10, 10, 0)
            };

        sale.SetItems(items);

        _productRepository.GetAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(product);

        await _salePricing.Pricing(sale);

        Assert.Equal(product.Price, sale.Items.First().Price);
    }

    [Fact(DisplayName = "Given invalid product data When apply pricing Then should have error")]
    public async Task Handle_InvalidProduct_ShouldHaveError()
    {
        var sale = new Sale();

        var items = new List<SaleItem>
            {
                new SaleItem(0, new Product { Id = 0 }, 0, 10, 10, 0)
            };

        sale.SetItems(items);

        _productRepository.GetAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).ReturnsNull();

        await Assert.ThrowsAsync<InvalidOperationException>(() => _salePricing.Pricing(sale));
    }
}
