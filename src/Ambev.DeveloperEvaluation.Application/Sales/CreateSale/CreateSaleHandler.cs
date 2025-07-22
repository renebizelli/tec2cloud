using Ambev.DeveloperEvaluation.Application.Sales._Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services.Sales.Discounts;
using Ambev.DeveloperEvaluation.Domain.Services.Sales.Pricing;
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
    private readonly ISaleDiscountApplier _discountApplier;
    private readonly IBus _bus;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSaleHandler> _logger;

    public CreateSaleHandler(
        ISaleRepository saleRepository,
        ICartRepository cartRepository,
        ISalePricing salePricing,
        ISaleDiscountApplier discountApplier,
        Rebus.Bus.IBus bus,
        IMapper mapper,
        ILogger<CreateSaleHandler> logger)
    {
        _saleRepository = saleRepository;
        _cartRepository = cartRepository;
        _salePricing = salePricing;
        _discountApplier = discountApplier;
        _bus = bus;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<SaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var filter = _mapper.Map<CartFilter>(command);

        var validationResult = await filter.ValidateAsync(cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var cart = await GetCart(filter, cancellationToken);

        var sale = _mapper.Map<Sale>(cart);

        SaleItemAllowedQuantityValidation(sale);

        await _salePricing.Pricing(sale);

        _discountApplier.Applier(sale);

        await _saleRepository.CreateSale(sale, cancellationToken);

        await _cartRepository.DeleteCart(filter, cancellationToken);

        await _bus.Send(new SaleCreatedEvent { SaleId = sale.Id });

        sale = await _saleRepository.Get(sale.Id, cancellationToken);

        var result = _mapper.Map<SaleResult>(sale);

        return result;
    }

    private async Task<Cart> GetCart(CartFilter filter, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetCartByUser(filter, cancellationToken);

        if (cart == null) throw new InvalidOperationException($"Empty cart");

        return cart;
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
}
