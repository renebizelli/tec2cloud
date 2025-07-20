using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCartByUser;

public class GetCartByUserHandle : IRequestHandler<GetCartByUserCommand, GetCartByUserResult>
{
    private readonly ICartRepository _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetCartByUserHandle
    /// </summary>
    /// <param name="cartRepository">The Cart repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for GetCartByUserCommand</param>
    public GetCartByUserHandle(
        ICartRepository cartRepository,
        IMapper mapper)
    {
        _repository = cartRepository;
        _mapper = mapper;
    }

    public async Task<GetCartByUserResult> Handle(GetCartByUserCommand command, CancellationToken cancellationToken)
    {
        var filter = _mapper.Map<CartFilter>(command);

        var cart = await _repository.GetCartByUser(filter, cancellationToken);

        if (cart == null) return new();

        var result = _mapper.Map<GetCartByUserResult>(cart);

        return result;
    }
}
