using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales._Shared;

public class SaleResult
{
    public int Id { get; set; }
    public Guid BranchId { get; set; }
    public SaleStatus Status { get; set; }
    public UserResult User { get; set; } = new();
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<SaleItemResult> Items { get; set; } = [];
}
