using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales._Shared.Responses;
public class SaleDetailResponse : SaleResponse
{
    public ICollection<SaleItemResponse> Items { get; set; } = [];
}
