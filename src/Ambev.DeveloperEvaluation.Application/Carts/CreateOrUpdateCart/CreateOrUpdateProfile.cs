using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateOrUpdateCart;

internal class CreateOrUpdateProfile : Profile
{
    public CreateOrUpdateProfile()
    {
        CreateMap<CreateOrUpdateCartCommand, Cart>();
        CreateMap<CreateOrUpdateCartCommand.CartItem, CartItem>();
    }
}
