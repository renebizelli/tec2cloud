using Ambev.DeveloperEvaluation.Domain.Common;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class CartFilterValidator : AbstractValidator<CartFilter>
{
    public CartFilterValidator()
    {
        RuleFor(cartFilter => cartFilter.UserId).NotEqual(Guid.Empty);
        RuleFor(cartFilter => cartFilter.BranchId).NotEqual(Guid.Empty);
    }
}
