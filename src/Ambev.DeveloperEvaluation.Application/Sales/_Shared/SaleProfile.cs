using Ambev.DeveloperEvaluation.Application.Sales._Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales._Shared;

internal class SaleProfile : Profile
{
    public SaleProfile()
    {
        CreateMap<Sale, SaleResult>()
            .ForMember(f => f.Branch, o => o.MapFrom(m => m.Branch));

        CreateMap<SaleItem, SaleItemResult>();

        CreateMap<User, UserResult>()
            .ForMember(f => f.Name, a => a.MapFrom(m => m.Username));

        CreateMap<Product, ProductResult>()
            .ForMember(f => f.Name, a => a.MapFrom(m => m.Title));

        CreateMap<Branch, BranchResult>();
    }
}
