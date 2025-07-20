using Ambev.DeveloperEvaluation.Application.Carts.GetCartByUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartByUser;

public class GetCartByUserProfile : Profile
{
    public GetCartByUserProfile()
    {
        CreateMap<GetCartByUserResult, GetCartByUserResponse>();
        CreateMap<GetCartByUserResult.CartItem, GetCartByUserResponse.CartItem>();
    }
}
