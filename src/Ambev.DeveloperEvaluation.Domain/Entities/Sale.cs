using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale
{
    public int Id { get; set; }
    public Guid BranchId { get; set; }
    public Branch? Branch { get; set; }
    public SaleStatus Status { get; set; } 
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public decimal TotalAmount => _totalAmount;
    private decimal _totalAmount;
    public DateTime CreatedAt { get; set; }
    
    private readonly List<SaleItem> _items = new();
    public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();

    public void TotalAmountCalculate()
    {
        _totalAmount = ActiveItems().Sum(s => s.TotalPrice);
    }

    public void AddItem(SaleItem item) {
        _items.Add(item);

        TotalAmountCalculate();
    }

    public void Cancel()
    {
        Status = SaleStatus.Cancelled;
    }

    public ICollection<SaleItem> ActiveItems()
    => Items.Where(w => w.Status == SaleItemStatus.Active).ToList();
}
