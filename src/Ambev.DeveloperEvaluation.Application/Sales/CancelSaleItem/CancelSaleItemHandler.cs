using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services.Sales.Discounts;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Rebus.Bus;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;

public class CancelSaleItemHandler : IRequestHandler<CancelSaleItemCommand, CancelSaleItemResult>
{
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly ISaleRepository _saleRepository;
    private readonly ISalePricing _salePricing;
    private readonly IBus _bus;
    private readonly ILogger<CancelSaleItemHandler> _logger;

    public CancelSaleItemHandler(ISaleItemRepository saleItemRepository, ISaleRepository saleRepository, IBus bus, ILogger<CancelSaleItemHandler> logger, ISalePricing salePricing)
    {
        _saleItemRepository = saleItemRepository;
        _saleRepository = saleRepository;
        _bus = bus;
        _logger = logger;
        _salePricing = salePricing;
    }

    public async Task<CancelSaleItemResult> Handle(CancelSaleItemCommand request, CancellationToken cancellationToken)
    {
        var validator = new CancelSaleItemCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = await _saleRepository.GetAttachedAsync(request.SaleId, cancellationToken);
        ArgumentNullException.ThrowIfNull(sale);

        var item = sale.Items.FirstOrDefault(f => f.Id == request.SaleItemId);
        if (item == null) throw new KeyNotFoundException($"Sale Item with ID {request.SaleItemId} not found");

        item.Cancel();

        _salePricing.Applier(sale);

        await _saleRepository.UpdateAsync(sale, cancellationToken); 

        await _bus.Send(new SaleItemCancelledEvent(request.SaleId, request.SaleItemId));

        return new CancelSaleItemResult { Success = true };
    }
}
