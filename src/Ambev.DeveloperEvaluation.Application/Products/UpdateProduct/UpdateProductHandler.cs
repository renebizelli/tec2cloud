using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateProductHandler> _logger;

    public UpdateProductHandler(IProductRepository productRepository, IMapper mapper, ILogger<UpdateProductHandler> logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[UpdateProduct] Start - ProductId {Id}", command.Id);

        var validator = new UpdateProductCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var foundProduct = await _productRepository.GetAsync(command.Id, cancellationToken);
        if (foundProduct == null)
            throw new KeyNotFoundException($"Product with ID {command.Id} not found");

        _mapper.Map(command, foundProduct);

        await _productRepository.UpdateAsync(foundProduct, cancellationToken);

        var result = _mapper.Map<UpdateProductResult>(foundProduct);

        _logger.LogInformation("[UpdateProduct] Finish - ProductId {Id}", command.Id);

        return result;
    }
}
