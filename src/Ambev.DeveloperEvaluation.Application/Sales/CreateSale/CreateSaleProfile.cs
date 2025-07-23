using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
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
            .ForMember(f => f.CreatedAt, a => a.MapFrom(m => DateTime.UtcNow))
            .ForMember(f => f.Items, a => a.Ignore())
            .AfterMap((src, desc, context) =>
            {
                foreach (var item in src.Items)
                {
                    var saleItem = context.Mapper.Map<SaleItem>(item);
                    desc.AddItem(saleItem);
                }

            });

        CreateMap<CartItem, SaleItem>()
            .ForMember(f => f.Id, a => a.Ignore())
            .ForMember(f => f.ProductId, a => a.MapFrom(m => m.ProductId))
            .ForMember(f => f.Quantity, a => a.MapFrom(m => m.Quantity))
            .ForMember(f => f.Price, a => a.MapFrom<SaleItemPrice>())
            .ForMember(f => f.Discount, a => a.Ignore())
            .ForMember(f => f.Status, a => a.MapFrom(m => SaleItemStatus.Active));
    }
}
 
public class SaleItemPrice : IValueResolver<CartItem, SaleItem, decimal>
{
    private readonly IProductRepository _productRepository;

    public SaleItemPrice(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public decimal Resolve(CartItem source, SaleItem destination, decimal destMember, ResolutionContext context)
    => (decimal)context.Items[source.ProductId.ToString()];
}

