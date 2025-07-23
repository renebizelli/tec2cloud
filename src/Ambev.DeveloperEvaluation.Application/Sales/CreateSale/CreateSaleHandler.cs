using Ambev.DeveloperEvaluation.Application.Sales._Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services.Sales.Discounts;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Rebus.Bus;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, SaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ICartRepository _cartRepository;
    private readonly ISalePricing _salePricing;
    private readonly IProductRepository _productRepository;
    private readonly IBus _bus;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSaleHandler> _logger;

    public CreateSaleHandler(
        ISaleRepository saleRepository,
        ICartRepository cartRepository,
        ISalePricing salePricing,
        IProductRepository productRepository,
        Rebus.Bus.IBus bus,
        IMapper mapper,
        ILogger<CreateSaleHandler> logger)
    {
        _saleRepository = saleRepository;
        _cartRepository = cartRepository;
        _salePricing = salePricing;
        _productRepository = productRepository;
        _bus = bus;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<SaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[CreateSale] Start - UserId {UserId}, BranchId {BranchId}", command.UserId, command.BranchId);

        var filter = _mapper.Map<CartFilter>(command);

        var validationResult = await filter.ValidateAsync(cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = await SaleMappingAsync(filter, cancellationToken);

        SaleItemAllowedQuantityValidation(sale);

        CompletedSaleValidation(sale);

        _salePricing.Applier(sale);

        await _saleRepository.CreateSaleAsync(sale, cancellationToken);

        await _cartRepository.DeleteCart(filter, cancellationToken);

        await _bus.Send(new SaleCreatedEvent(sale.Id));

        sale = await _saleRepository.GetAsync(sale.Id, cancellationToken);

        var result = _mapper.Map<SaleResult>(sale);

        _logger.LogInformation("[CreateSale] Finish - UserId {UserId}, BranchId {BranchId}", command.UserId, command.BranchId);

        return result;
    }

    private async Task<Sale> SaleMappingAsync(CartFilter filter, CancellationToken cancellationToken)
    {
        var cart = await GetCartAsync(filter, cancellationToken);

        Dictionary<string, object> prices = new();
        foreach (var item in cart.Items)
        {
            var product = await _productRepository.GetAsync(item.ProductId, cancellationToken);
            if (product == null) throw new InvalidOperationException($"Invalid product id {item.ProductId}");
            prices[item.ProductId.ToString()] = product.Price;
        }

        var sale = _mapper.Map<Sale>(cart, o =>
        {
            foreach (var price in prices)
            {
                o.Items[price.Key] = price.Value;
            }
        });

        return sale;
    }

    private async Task<Cart> GetCartAsync(CartFilter filter, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetCartByUser(filter, cancellationToken);

        return cart ?? throw new InvalidOperationException($"Empty cart");
    }

    private void SaleItemAllowedQuantityValidation(Sale sale)
    {
        var spec = new SaleItemAllowedQuantitySpecification();
        foreach (var item in sale.Items)
        {
            if (!spec.IsSatisfiedBy(item.Quantity))
            {
                throw new DomainException("Invalid quantity of item");
            }
        }
    }

    private void CompletedSaleValidation(Sale sale)
    {
        var spec = new CompletedSaleSpecification();
        if (!spec.IsSatisfiedBy(sale)) throw new DomainException("There is no items");
    }
}
