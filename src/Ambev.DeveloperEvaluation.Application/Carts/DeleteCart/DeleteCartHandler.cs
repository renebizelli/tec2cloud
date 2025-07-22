using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

public class DeleteCartHandler : IRequestHandler<DeleteCartCommand>
{
    private readonly ICartRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteCartHandler> _logger;

    public DeleteCartHandler(
        ICartRepository cartRepository,
        IMapper mapper,
        ILogger<DeleteCartHandler> logger)
    {
        _repository = cartRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(DeleteCartCommand command, CancellationToken cancellationToken)
    {
        var filter = _mapper.Map<CartFilter>(command);

        var validationResult = await filter.ValidateAsync(cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sucess = await _repository.DeleteCart(filter, cancellationToken);

        if (!sucess) throw new KeyNotFoundException($"Cart not found with UserId {command.UserId} and branchId {command.BranchId}");
    }
}
