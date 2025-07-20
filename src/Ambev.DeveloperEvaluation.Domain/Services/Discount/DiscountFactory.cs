namespace Ambev.DeveloperEvaluation.Domain.Services.Discount;

public class DiscountFactory
{
    public static IDiscount Get(int quantity)
        => quantity switch
        {
            >= 4 and <= 9 => new Discount10(),
            >= 10 and <= 20 => new Discount20(),
            _ => new DefaultDiscount()
        };

}
