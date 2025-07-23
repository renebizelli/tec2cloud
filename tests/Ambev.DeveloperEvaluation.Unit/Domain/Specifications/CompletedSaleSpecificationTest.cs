using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications;

public class CompletedSaleSpecificationTest
{
    [Fact(DisplayName = "Given a complete sale then It shoud be valid")]
    public void IsSatisfiedBy_Should_Validate_Sales_With_At_Least_One_Items()
    {
        var sale = new Sale();
        sale.AddItem(new SaleItem(1, new(), 1, 10, 0, 0));
        var specification = new CompletedSaleSpecification();
        var result = specification.IsSatisfiedBy(sale);
        result.Should().BeTrue();
    }

    [Fact(DisplayName = "Given a sale emty items then It shoud not be valid")]
    public void IsSatisfiedBy_Should_Validate_Sales_With_Any_Items()
    {
        var sale = new Sale();
        var specification = new CompletedSaleSpecification();
        var result = specification.IsSatisfiedBy(sale);
        result.Should().BeFalse();
    }
}
