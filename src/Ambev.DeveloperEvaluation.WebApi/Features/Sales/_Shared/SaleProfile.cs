using Ambev.DeveloperEvaluation.Application.Sales._Shared.Results;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales._Shared.Responses;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales._Shared
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<SaleResult, SaleResponse>()
                .ForMember(f => f.Branch, o => o.MapFrom(m => m.Branch));

            CreateMap<SaleItemResult, SaleItemResponse>();
            CreateMap<UserResult, UserResponse>();
            CreateMap<ProductResult, ProductResponse>();
            CreateMap<BranchResult, BranchResponse>();
        }
    }
}
