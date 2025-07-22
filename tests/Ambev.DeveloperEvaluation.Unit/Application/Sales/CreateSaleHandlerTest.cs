using Ambev.DeveloperEvaluation.Application.Carts.CreateOrUpdateCart;
using Ambev.DeveloperEvaluation.Application.Sales._Shared.Results;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services.Sales.Discounts;
using Ambev.DeveloperEvaluation.Domain.Services.Sales.Pricing;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Rebus.Bus;
using System.Threading;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class CreateSaleHandlerTest
{
    private readonly ISaleRepository _saleRepository;
    private readonly ICartRepository _cartRepository;
    private readonly ISalePricing _salePricing;
    private readonly ISaleDiscountApplier _discountApplier;
    private readonly IBus _bus;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSaleHandler> _logger;
    private readonly CreateSaleHandler _handler;

    public CreateSaleHandlerTest()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _cartRepository = Substitute.For<ICartRepository>();
        _salePricing = Substitute.For<ISalePricing>();
        _discountApplier = Substitute.For<ISaleDiscountApplier>();
        _bus = Substitute.For<IBus>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<CreateSaleHandler>>();
        _handler = new CreateSaleHandler(_saleRepository, _cartRepository, _salePricing, _discountApplier, _bus, _mapper, _logger);
    }

    [Fact(DisplayName = "Given valid cart filter data When creating sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        var command = CreateSaleHandlerTestData.GenerateValidCommand();

        var cart = new Cart
        {
            UserId = command.UserId,
            BranchId = command.BranchId,
            Items = new List<CartItem>
             {
                 new CartItem
                 {
                     ProductId = 1, Quantity = 1
                 }
             }
        };

        var sale = new Sale
        {
            Id = 1,
            BranchId = cart.BranchId,
            UserId = cart.UserId,
            Status = SaleStatus.Active,
            Items = cart.Items.Select(s => new SaleItem { Quantity = s.Quantity, ProductId = s.ProductId  }).ToList(),
            TotalAmount = 10,
            CreatedAt = DateTime.UtcNow
        };

        var cartFilter = new CartFilter
        {
            BranchId = command.BranchId,
            UserId = command.UserId
        };

        var result = new SaleResult
        {
            Branch = new BranchResult
            {
                Id = command.BranchId
            },
            User = new UserResult
            {
                Id = command.UserId,
            },
            Id = 1,
            Status = DeveloperEvaluation.Domain.Enums.SaleStatus.Active,
            Items = new List<SaleItemResult>
            {
                new SaleItemResult {  Discount = 0, Status = SaleItemStatus.Active, Price = 10, Id = 1, Product = new ProductResult { Id = 1 }, Quantity = 1 },
            }
        };

        _mapper.Map<CartFilter>(command).Returns(cartFilter);
        _mapper.Map<Sale>(cart).Returns(sale);
        _mapper.Map<SaleResult>(sale).Returns(result);

        _cartRepository.GetCartByUser(Arg.Any<CartFilter>(), Arg.Any<CancellationToken>()).Returns(cart);
       _saleRepository.Get(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(sale);

        var resultHandle = await _handler.Handle(command, CancellationToken.None);

        await _salePricing.Received(1).Pricing(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        _discountApplier.Received(1).Applier(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        await _saleRepository.Received(1).CreateSale(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        await _cartRepository.Received(1).DeleteCart(Arg.Any<CartFilter>(), Arg.Any<CancellationToken>());
        await _bus.Received(1).Send(Arg.Any<SaleCreatedEvent>());

        Assert.NotNull(resultHandle);
    }

    [Fact(DisplayName = "Given invalid command data When creating sale Then throws validation exception")]
    public async Task Handle_InvalidCommand_ThrowsValidationException()
    {
        var command = new CreateSaleCommand();

        _mapper.Map<CartFilter>(command).Returns(new CartFilter());

        var act = () => _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }


}
