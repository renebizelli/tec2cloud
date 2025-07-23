using Ambev.DeveloperEvaluation.WebApi.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;

public class ListSalesRequest : ListRequest
{
    public Guid UserId { get; set; }
    public Guid BranchId { get; set; }
    public DateTime MinDate { get; set; } = DateTime.MinValue;
    public DateTime MaxDate { get; set; } = DateTime.MinValue;
    public decimal MinTotalPrice { get; set; }
    public decimal MaxTotalPrice { get; set; }
}
