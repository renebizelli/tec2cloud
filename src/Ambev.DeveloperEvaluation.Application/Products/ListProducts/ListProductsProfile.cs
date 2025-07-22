using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

internal class ListProductsProfile : Profile
{
    public ListProductsProfile()
    {
        CreateMap<(int TotalCount, IList<Product> Products), ProductsResult>()
            .ForMember(f => f.TotalCount, o => o.MapFrom(m => m.TotalCount))
            .ForMember(f => f.Products, o => o.MapFrom(m => m.Products));

        CreateMap<Product, ProductResult>();
    }
}
