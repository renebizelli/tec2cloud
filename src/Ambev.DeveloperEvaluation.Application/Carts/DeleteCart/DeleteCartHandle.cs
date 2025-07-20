using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

internal class DeleteCartHandle : IRequestHandler<DeleteCartCommand>
{
    private readonly ICartRepository _repository;
    private readonly IMapper _mapper;

    public DeleteCartHandle(
        ICartRepository cartRepository,
        IMapper mapper)
    {
        _repository = cartRepository;
        _mapper = mapper;
    }

    public async Task Handle(DeleteCartCommand command, CancellationToken cancellationToken)
    {
        var filter = _mapper.Map<CartFilter>(command);

        await _repository.DeleteCart(filter, cancellationToken);
    }
}
