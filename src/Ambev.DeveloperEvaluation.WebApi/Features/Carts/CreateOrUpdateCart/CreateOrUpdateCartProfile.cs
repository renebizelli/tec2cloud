using Ambev.DeveloperEvaluation.Application.Carts.CreateOrUpdateCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateOrUpdateCart;

public class CreateOrUpdateCartProfile : Profile
{
    public CreateOrUpdateCartProfile()
    {
        CreateMap<CreateOrUpdateCartRequest, CreateOrUpdateCartCommand>()
                .ForMember(f => f.UpdatedAt, v => v.MapFrom(f => DateTime.Now));

        CreateMap<CreateOrUpdateCartRequest.CartItem, CreateOrUpdateCartCommand.CartItem>();
    }
}
