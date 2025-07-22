using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Services.Sales.Discounts;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Services;

public class SaleDiscountApplierTest
{
    private readonly SaleDiscountApplier _saleDiscountApplier;

    public SaleDiscountApplierTest()
    {
        _saleDiscountApplier = new SaleDiscountApplier();
    }


    [Fact(DisplayName = "Given valid sale allowed to discount When apply discounts")]
    public void Handle_ValidProduct_ShouldNotHaveError()
    {
        var sale = new Sale
        {
            Items = new List<SaleItem>
            {
                new SaleItem
                {
                    Quantity = 1,
                    Price = 10,
                    Status = SaleItemStatus.Active,
                },
                new SaleItem
                {
                    Quantity = 4,
                    Price = 10,
                    Status = SaleItemStatus.Active,
                },
                new SaleItem
                {
                    Quantity = 9,
                    Price = 10,
                    Status = SaleItemStatus.Active,
                },
                new SaleItem
                {
                    Quantity = 11,
                    Price = 10,
                    Status = SaleItemStatus.Active,
                },
                new SaleItem
                {
                    Quantity = 19,
                    Price = 10,
                    Status = SaleItemStatus.Active,
                }
            }
        };

        _saleDiscountApplier.Applier(sale);

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
    }


    [Fact(DisplayName = "Given a sale with less items than allowed to have discount then no apply discount")]
    public void Handle_Less_Items_Allowed_Discount_No_Apply_Discount()
    {
        var sale = new Sale
        {
            Items = new List<SaleItem>
            {
                new SaleItem
                {
                    Quantity = 10,
                    Price = 10,
                    Status = SaleItemStatus.Active,
                },
            }
        };

        for (int i = 0; i < 5; i++)
        {
            sale.Items.Add(new SaleItem { Quantity = 10, Price = 10, Status = SaleItemStatus.Cancelled });
        }

        _saleDiscountApplier.Applier(sale);

        foreach (var item in sale.Items)
        {
            Assert.Equal(0, item.Discount);
            Assert.Equal(item.Price * item.Quantity, item.TotalPrice);
        }
    }
}
