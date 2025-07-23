using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services.Sales.Discounts;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Services;

public class SalePricingTest
{
    private readonly SalePricing _salePricing;

    public SalePricingTest()
    {
        _salePricing = new SalePricing();
    }


    [Fact(DisplayName = "Given valid sale allowed to discount When apply discounts")]
    public void Handle_ValidProduct_ShouldNotHaveError()
    {
        var sale = new Sale();

        sale.AddItem(new SaleItem(0, new Product { Id = 1 }, 1, 10, 0, 0));
        sale.AddItem(new SaleItem(0, new Product { Id = 1 }, 4, 10, 0, 0));
        sale.AddItem(new SaleItem(0, new Product { Id = 1 }, 9, 10, 0, 0));
        sale.AddItem(new SaleItem(0, new Product { Id = 1 }, 11, 10, 0, 0));
        sale.AddItem(new SaleItem(0, new Product { Id = 1 }, 19, 10, 0, 0));

        _salePricing.Applier(sale);

        var source = new List<(int quantity, decimal percent)>()
        {
            new (1, 0),
            new (4, 0.1M),
            new (9, 0.1M),
            new (11, 0.2M),
            new (19, 0.2M),
        };

        foreach (var (quantity, percent) in source)
        {
            var item = sale.Items.Where(w => w.Quantity == quantity).First();
            Assert.Equal(item.Price * percent, item.Discount);
            Assert.Equal((item.Price * item.Quantity) - item.Discount, item.TotalPrice);
        }

        Assert.Equal(434, sale.TotalAmount);
    }


    [Fact(DisplayName = "Given a sale with less items then allowed to have discount then no apply discount")]
    public void Handle_Less_Items_Allowed_Discount_No_Apply_Discount()
    {
        var sale = new Sale();
        
        sale.AddItem(new SaleItem(0, new Product { Id = 1 }, 10, 10, 0, 0));

        for (int i = 0; i < 5; i++)
        {
            var item = new SaleItem(0, new Product { Id = 1 }, 10, 10, 0, 0);
            item.Cancel();

            sale.AddItem(item);
        }

        _salePricing.Applier(sale);

        foreach (var item in sale.Items)
        {
            Assert.Equal(0, item.Discount);
            Assert.Equal(item.Price * item.Quantity, item.TotalPrice);
        }

        Assert.Equal(100, sale.TotalAmount);
    }


    [Fact(DisplayName = "Given a sale with some cancelled items then pricing must ignore them")]
    public void Handle_Some_Cancelled_Items_Igonre_Them()
    {
        var sale = new Sale();

        sale.AddItem(new SaleItem(0, new Product { Id = 1 }, 10, 10, 0, 0));
        sale.AddItem(new SaleItem(0, new Product { Id = 1 }, 10, 10, 0, 0));
        sale.AddItem(new SaleItem(0, new Product { Id = 1 }, 10, 10, 0, 0));
        sale.AddItem(new SaleItem(0, new Product { Id = 1 }, 10, 10, 0, 0));
        sale.AddItem(new SaleItem(0, new Product { Id = 1 }, 10, 10, 0, 0));

        for (int i = 0; i < 5; i++)
        {
            var item = new SaleItem(0, new Product { Id = 1 }, 10, 10, 0, 0);
            item.Cancel();

            sale.AddItem(item);
        }

        _salePricing.Applier(sale);

        foreach (var item in sale.Items)
        {
            Assert.Equal((item.Price * item.Quantity) - item.Discount, item.TotalPrice);
        }

        Assert.Equal(490, sale.TotalAmount);
    }
}
