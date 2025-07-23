using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

public class CartFilterValidatorTest
{
    private readonly CartFilterValidator _validator;

    public CartFilterValidatorTest()
    {
        _validator = new CartFilterValidator();
    }

    [Fact(DisplayName = "Valid cart filter should pass validation")]
    public void Given_Valid_CartFilter_When_Validated_Then_ShouldNotHaveErrors()
    {
        var filter = new CartFilter
        {
            UserId = Guid.NewGuid(),
            BranchId = Guid.NewGuid()
        };

        var result = _validator.TestValidate(filter);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "Invalid branchId in cart filter should not pass validation")]
    public void Given_Invalid_BranchId_CartFilter_When_Validated_Then_ShouldHaveErrors()
    {
        var filter = new CartFilter
        {
            UserId = Guid.NewGuid(),
        };

        var result = _validator.TestValidate(filter);

        result.ShouldHaveValidationErrorFor(x => x.BranchId);
    }

    [Fact(DisplayName = "Invalid userid in cart filter should not pass validation")]
    public void Given_Invalid_UserId_CartFilter_When_Validated_Then_ShouldHaveErrors()
    {
        var filter = new CartFilter
        {
            BranchId = Guid.NewGuid(),
        };

        var result = _validator.TestValidate(filter);

        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }

}
