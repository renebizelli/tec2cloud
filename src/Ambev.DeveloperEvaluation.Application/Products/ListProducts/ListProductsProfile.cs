﻿using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

public class ListProductsProfile : Profile
{
    public ListProductsProfile()
    {
        CreateMap<(int TotalCount, IList<Product> Products), PaginatedResult<ProductResult>>()
            .ForMember(f => f.TotalCount, o => o.MapFrom(m => m.TotalCount))
            .ForMember(f => f.Items, o => o.MapFrom(m => m.Products));

        CreateMap<Product, ProductResult>();
    }
}
