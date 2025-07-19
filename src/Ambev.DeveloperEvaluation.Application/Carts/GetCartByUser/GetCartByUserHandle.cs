using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Entities;

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

    public async Task<GetCartByUserResult> Handle(GetCartByUserCommand request, CancellationToken cancellationToken)
    {
        var cart = new Cart
        {
            UserId = Guid.Parse("ad4edb9d-73ef-43ff-9344-3c142958740c"),
            UpdatedAt = DateTime.Today,
            Items = new List<CartItem>()
            {
                new CartItem { ProductId = 1, Quantity = 3 },
            }
        };

        await _repository.CreateOrUpdateCart(cart, cancellationToken);


        var s = await _repository.GetCartByUser(request.UserId, cancellationToken);

        return new GetCartByUserResult();
    }
}
