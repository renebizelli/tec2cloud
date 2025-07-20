using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public interface ISalePricing
{
    Task Pricing(Sale sale, CancellationToken cancellationToken = default);
}