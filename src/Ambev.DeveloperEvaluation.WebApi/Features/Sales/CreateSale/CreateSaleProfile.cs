using Ambev.DeveloperEvaluation.Application.Sales._Shared;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales._Shared;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleRequest, CreateSaleCommand>();
        CreateMap<CreateSaleCommand, CartFilter>();

        CreateMap<SaleResult, SaleResponse>();
        CreateMap<SaleItemResult, SaleItemResponse>();
        CreateMap<UserResult, UserResponse>();
        CreateMap<ProductResult, ProductResponse>();

    }
}
