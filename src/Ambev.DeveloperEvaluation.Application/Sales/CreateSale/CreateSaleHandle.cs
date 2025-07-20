using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.Services.Discount;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleHandle : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ICartRepository _cartRepository;
    private readonly ISalePricing _salePricing;
    private readonly IMapper _mapper;

    public CreateSaleHandle(
        ISaleRepository saleRepository,
        ICartRepository cartRepository,
        ISalePricing salePricing,
        IMapper mapper)
    {
        _saleRepository = saleRepository;
        _cartRepository = cartRepository;
        _salePricing = salePricing;
        _mapper = mapper;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var filter = _mapper.Map<CartFilter>(command);

        var cart = await GetCart(filter, cancellationToken);

        var sale = _mapper.Map<Sale>(cart);

        SaleItemQuantityValidation(sale);

        await _salePricing.Pricing(sale);

        await _saleRepository.CreateSale(sale, cancellationToken);

        await _cartRepository.DeleteCart(filter, cancellationToken);

        sale = await _saleRepository.Get(sale.Id, filter.UserId, cancellationToken);

        var result = _mapper.Map<CreateSaleResult>(sale);

        return result;
    }

    private async Task<Cart> GetCart(CartFilter filter, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetCartByUser(filter, cancellationToken);

        if (cart == null) throw new InvalidOperationException($"Empty cart");

        return cart;
    }

    private static void SaleItemQuantityValidation(Sale sale)
    {
        var saleItemQuantitySpec = new SaleItemQuantitySpecification();
        foreach (var item in sale.Items)
        {
            if (!saleItemQuantitySpec.IsSatisfiedBy(item))
            {
                throw new DomainException("Invalid quantity of item");
            }
        }
    }
}
