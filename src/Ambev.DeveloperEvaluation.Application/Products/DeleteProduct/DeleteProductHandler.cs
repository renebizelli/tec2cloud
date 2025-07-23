using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, DeleteProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<DeleteProductHandler> _logger;

    public DeleteProductHandler(IProductRepository productRepository, ILogger<DeleteProductHandler> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[DeleteProduct] Start - ProductId {Id}", command.Id);

        var validator = new DeleteProductCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _productRepository.DeleteAsync(command.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Product with ID {command.Id} not found");

        _logger.LogInformation("[DeleteProduct] Finish - ProductId {Id}", command.Id);

        return new DeleteProductResult { Success = true };
    }
}
