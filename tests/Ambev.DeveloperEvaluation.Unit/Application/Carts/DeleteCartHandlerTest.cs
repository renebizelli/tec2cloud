using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Threading;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

public class DeleteCartHandlerTest
{

    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteCartHandler> _logger;
    private readonly DeleteCartHandler _handler;

    public DeleteCartHandlerTest()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<DeleteCartHandler>>();
        _handler = new DeleteCartHandler(_cartRepository, _mapper, _logger);
    }

    [Fact(DisplayName = "Given valid userId and branchId When deleting cart Then returns success")]
    public async Task Handle_ValidRequest_ReturnsSuccess()
    {
        // Given
        var command = DeleteCartHandlerTestData.GenerateValidCommand();
        var cartFilter = new CartFilter()
        {
            BranchId = command.BranchId,
            UserId = command.UserId,
        };

        _mapper.Map<CartFilter>(command).Returns(cartFilter);

        _cartRepository.DeleteCart(Arg.Any<CartFilter>(), Arg.Any<CancellationToken>()).Returns(true);

        await _handler.Handle(command, CancellationToken.None);

        await _cartRepository.Received(1).DeleteCart(Arg.Any<CartFilter>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given a not found cart When deleting cart Then returns KeyNotFoundException")]
    public async Task Handle_NotFoundCartRequest_ReturnsKeyNotFoundException()
    {
        // Given
        var command = DeleteCartHandlerTestData.GenerateValidCommand();
        var cartFilter = new CartFilter()
        {
            BranchId = command.BranchId,
            UserId = command.UserId,
        };

        _mapper.Map<CartFilter>(command).Returns(cartFilter);

        _cartRepository.DeleteCart(Arg.Any<CartFilter>(), Arg.Any<CancellationToken>()).Returns(false);

        var act = () => _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>();
    }

}
