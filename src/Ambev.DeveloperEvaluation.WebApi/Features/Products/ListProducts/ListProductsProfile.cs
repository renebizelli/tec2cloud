using Ambev.DeveloperEvaluation.Application.Products.ListProducts;
using Ambev.DeveloperEvaluation.WebApi.Common;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;

public class ListProductsProfile : Profile
{
    public ListProductsProfile()
    {
        CreateMap<ListProductsRequest, Application.Products.ListProducts.ListProductsCommand>()
            .ForMember(f => f.OrderCriteria, o => o.MapFrom(m => OrderBuilder.Build(m._Order, "title")));

        CreateMap<ProductsResult, ProductsResponse>();
        CreateMap<ProductResult, ProductResponse>();
    }
}
