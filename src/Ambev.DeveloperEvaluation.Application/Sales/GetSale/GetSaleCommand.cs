using Ambev.DeveloperEvaluation.Application.Sales._Shared;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSaleCommand : IRequest<SaleResult>
{
    public int SaleId { get; set; }
}
