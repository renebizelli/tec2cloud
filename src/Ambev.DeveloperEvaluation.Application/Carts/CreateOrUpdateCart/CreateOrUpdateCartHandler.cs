using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateOrUpdateCart;

public class CreateOrUpdateCartHandler : IRequestHandler<CreateOrUpdateCartCommand>
{
    private readonly ICartRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateOrUpdateCartHandler> _logger;

    public CreateOrUpdateCartHandler(
        ICartRepository cartRepository,
        IMapper mapper,
        ILogger<CreateOrUpdateCartHandler> logger)
    {
        _repository = cartRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(CreateOrUpdateCartCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[CreateOrUpdateCart] Start - UserId {UserId}, BranchId, {BranchId}", command.UserId, command.BranchId);

        var validator = new CreateOrUpdateCartCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var cart = _mapper.Map<Cart>(command);

        await _repository.CreateOrUpdateCart(cart, cancellationToken);

        _logger.LogInformation("[CreateOrUpdateCart] Finish - UserId {UserId}, BranchId, {BranchId}", command.UserId, command.BranchId);
    }
}
