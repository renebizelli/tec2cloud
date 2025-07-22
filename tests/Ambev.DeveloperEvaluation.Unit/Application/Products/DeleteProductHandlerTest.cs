using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

public class DeleteProductHandlerTest
{

    private readonly IProductRepository _productRepository;
    private readonly ILogger<DeleteProductHandler> _logger;
    private readonly DeleteProductHandler _handler;

    public DeleteProductHandlerTest()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _logger = Substitute.For<ILogger<DeleteProductHandler>>();
        _handler = new DeleteProductHandler(_productRepository, _logger);
    }

    [Fact(DisplayName = "not found product id should throw KeyNotFoundException")]
    public async Task Given_NotFoundProductId_When_Validated_Then_ShouldThrowKeyNotFoundException()
    {
        var command = DeleteProductHandlerTestData.GenerateValidCommand();

        _productRepository.DeleteAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(false);

        await Assert.ThrowsAsync<KeyNotFoundException>(() =>  _handler.Handle(command, CancellationToken.None));
    }

    [Fact(DisplayName = "Valid product id should returns DeleteProductResponse")]
    public async Task Given_ProductId_When_Validated_Then_ShouldReturnsDeleteProductResponse()
    {
        var command = DeleteProductHandlerTestData.GenerateValidCommand();

        _productRepository.DeleteAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(true);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.True(result.Success);
    }

}
