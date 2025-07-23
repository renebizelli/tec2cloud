using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Rebus.Bus;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IBus _bus;
    private readonly ILogger<CancelSaleHandler> _logger;

    public CancelSaleHandler(ISaleRepository saleRepository, Rebus.Bus.IBus bus, ILogger<CancelSaleHandler> logger)
    {
        _saleRepository = saleRepository;
        _bus = bus;
        _logger = logger;
    }

    public async Task<CancelSaleResult> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[CancelSale] Start - SaleId {Id}", request.Id);

        var validator = new CancelSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _saleRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Sale with ID {request.Id} not found");

        await _bus.Send(new SaleCancelledEvent(request.Id));

        _logger.LogInformation("[CancelSale] Finish - SaleId {Id}", request.Id);

        return new CancelSaleResult { Success = true };
    }
}
