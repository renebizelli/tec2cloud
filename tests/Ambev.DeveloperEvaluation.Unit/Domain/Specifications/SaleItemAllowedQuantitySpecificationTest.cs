using Ambev.DeveloperEvaluation.Domain.Specifications;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications;

public class SaleItemAllowedQuantitySpecificationTest
{

    [Theory]
    [InlineData(2, true)]
    [InlineData(21, false)]
    [InlineData(0, false)]
    public void IsSatisfiedBy_ShouldValidateCartsQuantityItems(int quantity, bool expectedResult)
    {
        var specification = new SaleItemAllowedQuantitySpecification();

        var result = specification.IsSatisfiedBy(quantity);

        result.Should().Be(expectedResult);
    }
}
