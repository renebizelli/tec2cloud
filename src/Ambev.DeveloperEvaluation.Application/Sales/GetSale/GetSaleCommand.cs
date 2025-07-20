using Ambev.DeveloperEvaluation.Application.Sales._Shared.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSaleCommand : IRequest<SaleResult>
{
    public int SaleId { get; set; }
}
