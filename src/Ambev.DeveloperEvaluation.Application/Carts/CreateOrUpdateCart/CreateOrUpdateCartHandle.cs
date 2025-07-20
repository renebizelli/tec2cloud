using Ambev.DeveloperEvaluation.Application.Carts.GetCartByUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateOrUpdateCart;

public class CreateOrUpdateCartHandle : IRequestHandler<CreateOrUpdateCartCommand>
{
    private readonly ICartRepository _repository;
    private readonly IMapper _mapper;

    public CreateOrUpdateCartHandle(
        ICartRepository cartRepository,
        IMapper mapper)
    {
        _repository = cartRepository;
        _mapper = mapper;
    }

    public async Task Handle(CreateOrUpdateCartCommand command, CancellationToken cancellationToken)
    {
        var cart = _mapper.Map<Cart>(command);

        await _repository.CreateOrUpdateCart(cart, cancellationToken);
    }
}
