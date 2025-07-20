using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale
{
    public int Id { get; set; }
    public Guid BranchId { get; set; }
    public SaleStatus Status { get; set; } 
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<SaleItem> Items { get; set; } = [];

    public void TotalAmountCalculate()
    {
        TotalAmount = Items.Sum(s => s.TotalPrice);
    }

    public ICollection<SaleItem> ActiveItems()
    {
        return Items.Where(w => w.Status == SaleItemStatus.Active).ToList();
    }
}
