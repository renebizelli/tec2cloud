using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Threading;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

public class UpdateProductHandlerTest
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateProductHandler> _logger;
    private readonly UpdateProductHandler _handler;

    public UpdateProductHandlerTest()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<UpdateProductHandler>>();
        _handler = new UpdateProductHandler(_productRepository, _mapper, _logger);
    }


    [Fact(DisplayName = "Given valid product data When updating product Then returns result object")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = UpdateProductHandlerTestData.GenerateValidCommand();
        var product = new Product
        {
            Id = command.Id,
            Title = command.Title,
            Description = command.Description,
            Price = command.Price,
            Category = command.Category,
            Image = command.Image,
            Active = true
        };

        var result = new UpdateProductResult
        {
            Title = command.Title,
            Description = command.Description,
            Price = command.Price,
            Category = command.Category,
            Image = command.Image,
        };

         _productRepository.GetAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(product);

        _mapper.Map<Product>(command).Returns(product);
        _mapper.Map<UpdateProductResult>(product).Returns(result);

        var updateProductResult = await _handler.Handle(command, CancellationToken.None);

        updateProductResult.Should().NotBeNull();
        await _productRepository.Received(1).UpdateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid product data When creating product Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        var command = new UpdateProductCommand();

        var act = () => _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }


    [Fact(DisplayName = "Given valid command When handling Then maps command to products entity")]
    public async Task Handle_ValidRequest_MapsCommandToProduct()
    {
        // Given
        var command = UpdateProductHandlerTestData.GenerateValidCommand();
        var product = new Product
        {
            Id = 2,
            Title = command.Title,
            Description = command.Description,
            Price = command.Price,
            Category = command.Category,
            Image = command.Image,
            Active = true
        };

        var updateProductResult = new UpdateProductResult
        {
            Title = command.Title,
            Description = command.Description,
            Price = command.Price,
            Category = command.Category,
            Image = product.Image,
        };

        _productRepository.GetAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(product);

        _mapper.Map<Product>(command).Returns(product);
        _mapper.Map<UpdateProductResult>(product).Returns(updateProductResult);

        var result =  await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(product.Title, result.Title);
        Assert.Equal(product.Description, result.Description);
        Assert.Equal(product.Price, result.Price);
        Assert.Equal(product.Category, result.Category);
        Assert.Equal(product.Image, result.Image);
    }
}
