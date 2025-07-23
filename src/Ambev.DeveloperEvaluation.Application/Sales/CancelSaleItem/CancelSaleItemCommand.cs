using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;

public class CancelSaleItemCommand : IRequest<CancelSaleItemResult>
{
    public int SaleId { get; set; }
    public int SaleItemId { get; set; }
}
