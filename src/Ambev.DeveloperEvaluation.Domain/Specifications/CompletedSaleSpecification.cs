using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public class CompletedSaleSpecification : ISpecification<Sale>
{
    public bool IsSatisfiedBy(Sale entity)
    => entity.ActiveItems().Any();
}
