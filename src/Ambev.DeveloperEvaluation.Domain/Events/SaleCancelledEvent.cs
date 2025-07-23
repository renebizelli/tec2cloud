namespace Ambev.DeveloperEvaluation.Domain.Events;

public class SaleCancelledEvent
{
    public int SaleId { get; set; }

    public SaleCancelledEvent(int saleId)
    {
        SaleId = saleId;
    }
}
