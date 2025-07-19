using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts;

internal class GetProductsProfile : Profile
{
    public GetProductsProfile()
    {
        CreateMap<Product, GetProductResult>();
    }
}
