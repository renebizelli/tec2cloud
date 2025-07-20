using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateOrUpdateCart;

public class CreateOrUpdateCartRequestValidator : AbstractValidator<CreateOrUpdateCartRequest>
{
    public CreateOrUpdateCartRequestValidator()
    {
        RuleForEach(d => d.Items)
            .ChildRules(item => {
                item.RuleFor(i => i.Quantity).SetValidator(new ItemQuantityValidator()).WithMessage("quantity must to be great than zero");
            });
    }
}
