using Ambev.DeveloperEvaluation.Application.Sales._Shared;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales._Shared;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

public class GetSaleProfile : Profile
{
    public GetSaleProfile()
    {
        CreateMap<GetSaleRequest, GetSaleCommand>();
        CreateMap<GetSaleCommand, CartFilter>();

        CreateMap<SaleResult, SaleResponse>();
        CreateMap<SaleItemResult, SaleItemResponse>();
        CreateMap<UserResult, UserResponse>();
        CreateMap<ProductResult, ProductResponse>();

    }
}
