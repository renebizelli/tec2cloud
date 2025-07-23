using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Sales._Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

public class ListSalesCommand : IRequest<PaginatedResult<SaleResult>>, ISalesQueryOptions
{
    public Guid UserId { get; set; }
    public Guid BranchId { get; set; }
    public DateTime MinDate { get; set; } = DateTime.MinValue;
    public DateTime MaxDate { get; set; } = DateTime.MinValue;
    public decimal MinTotalPrice { get; set; }
    public decimal MaxTotalPrice { get; set; }

    public List<(string, bool)> OrderCriteria { get; set; } = new();
    public int Page { get; set; }
    public int PageSize { get; set; }
}
