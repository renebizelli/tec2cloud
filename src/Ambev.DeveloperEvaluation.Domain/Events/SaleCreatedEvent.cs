namespace Ambev.DeveloperEvaluation.Domain.Events;

public class SaleCreatedEvent
{
    public int SaleId { get; set; }
    
    public SaleCreatedEvent(int saleId)
    {
        SaleId = saleId;
    }
}
