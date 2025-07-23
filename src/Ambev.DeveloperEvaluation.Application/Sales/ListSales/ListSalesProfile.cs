using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Sales._Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

internal class ListSalesProfile : Profile
{
    public ListSalesProfile()
    {
        CreateMap<(int TotalCount, IList<Sale> Sales), PaginatedResult<SaleResult>>()
            .ForMember(f => f.TotalCount, o => o.MapFrom(m => m.TotalCount))
            .ForMember(f => f.Items, o => o.MapFrom(m => m.Sales));

        CreateMap<Sale, SaleResult>();
    }
}



 