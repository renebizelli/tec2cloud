using Ambev.DeveloperEvaluation.Application.Sales._Shared.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommand : IRequest<SaleResult>
{
    public Guid UserId { get; set; }
    public Guid BranchId { get; set; }
}
