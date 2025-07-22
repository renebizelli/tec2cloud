using Ambev.DeveloperEvaluation.Domain.Specifications;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class ItemQuantityValidator : AbstractValidator<int>
{
    public ItemQuantityValidator()
    {
        RuleFor(item => item).Must(v => {
            var spec = new SaleItemAllowedQuantitySpecification();
            return spec.IsSatisfiedBy(v);
        });
    }
}
