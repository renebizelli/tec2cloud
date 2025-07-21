using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services.Sales.Discounts;

public interface ISaleDiscountApplier
{
    Task Applier(Sale sale, CancellationToken cancellationToken = default);
}
