using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales._Shared.Responses;
public class SaleResponse
{
    public int Id { get; set; }
    public BranchResponse Branch { get; set; } = new();
    public SaleStatus Status { get; set; }
    public UserResponse User { get; set; } = new();
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
}
