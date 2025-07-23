namespace Ambev.DeveloperEvaluation.Domain.Events;

public class SaleItemCancelledEvent
{
    public SaleItemCancelledEvent(int saleId, int saleItemId)
    {
        SaleId = saleId;
        SaleItemId = saleItemId;
    }

    public int SaleId { get; set; }
    public int SaleItemId { get; set; }
}
