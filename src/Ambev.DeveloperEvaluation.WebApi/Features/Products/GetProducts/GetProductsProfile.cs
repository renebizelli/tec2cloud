using Ambev.DeveloperEvaluation.Application.Products.GetProducts;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts;

public class GetProductsProfile : Profile
{
    public GetProductsProfile()
    {
        CreateMap<GetProductsRequest, Application.Products.GetProducts.GetProductsCommand>();
        CreateMap<GetProductResult, GetProductResponse>();
    }
}
