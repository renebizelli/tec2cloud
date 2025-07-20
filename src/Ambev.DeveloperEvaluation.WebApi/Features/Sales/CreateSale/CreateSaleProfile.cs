using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Common;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleRequest, CreateSaleCommand>();
        CreateMap<CreateSaleCommand, CartFilter>();

        CreateMap<CreateSaleResult, CreateSaleResponse>();
        CreateMap<SaleItemResult, SaleItemResponse>();
        CreateMap<UserResult, UserResponse>();
        CreateMap<ProductResult, ProductResponse>();

    }
}
