using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class ItemQuantityValidator : AbstractValidator<int>
{
    public ItemQuantityValidator()
    {
        RuleFor(item => item)
            .GreaterThan(0)
            .LessThanOrEqualTo(20);
    }
}
