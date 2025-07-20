using Ambev.DeveloperEvaluation.Application.Sales._Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<Cart, Sale>()
            .ForMember(f => f.Id, a => a.Ignore())
            .ForMember(f => f.UserId, a => a.MapFrom(m => m.UserId))
            .ForMember(f => f.BranchId, a => a.MapFrom(m => m.BranchId))
            .ForMember(f => f.Status, a => a.MapFrom(m => SaleStatus.Active))
            .ForMember(f => f.TotalAmount, a => a.Ignore())
            .ForMember(f => f.CreatedAt, a => a.MapFrom(m => DateTime.UtcNow))
            .ForMember(f => f.Items, a => a.MapFrom(m => m.Items));

        CreateMap<CartItem, SaleItem>()
            .ForMember(f => f.Id, a => a.Ignore())
            .ForMember(f => f.ProductId, a => a.MapFrom(m => m.ProductId))
            .ForMember(f => f.Quantity, a => a.MapFrom(m => m.Quantity))
            .ForMember(f => f.Price, a => a.Ignore())
            .ForMember(f => f.Discount, a => a.Ignore())
            .ForMember(f => f.Status, a => a.MapFrom(m => SaleItemStatus.Active));
    }
}

