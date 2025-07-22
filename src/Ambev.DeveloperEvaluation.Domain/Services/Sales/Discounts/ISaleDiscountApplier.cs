using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services.Sales.Discounts;

public interface ISaleDiscountApplier
{
    void Applier(Sale sale, CancellationToken cancellationToken = default);
}
