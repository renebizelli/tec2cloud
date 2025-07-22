using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

public class DeleteProductCommandValidatorTest
{
    private DeleteProductCommandValidator _validator;

    public DeleteProductCommandValidatorTest()
    {
        _validator = new DeleteProductCommandValidator();
    }


    [Fact(DisplayName = "Valid product id should pass validation")]
    public void Given_ValidProductId_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var command = DeleteProductHandlerTestData.GenerateValidCommand();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "Invalid product id should  fail validation")]
    public void Given_InvalidProductId_When_Validated_Then_ShouldHaveErrors()
    {
        // Arrange
        var command = DeleteProductHandlerTestData.GenerateInvalidCommand();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveAnyValidationError();
    }
}
