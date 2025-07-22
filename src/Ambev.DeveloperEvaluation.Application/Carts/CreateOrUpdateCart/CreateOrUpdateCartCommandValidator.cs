using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateOrUpdateCart;

public class CreateOrUpdateCartCommandValidator : AbstractValidator<CreateOrUpdateCartCommand>
{
    public CreateOrUpdateCartCommandValidator()
    {
        RuleFor(r => r.UserId).NotEmpty().WithMessage("invalid user"); 
        RuleFor(r => r.BranchId).NotEmpty().WithMessage("invalid branch"); 

        RuleForEach(d => d.Items)
            .ChildRules(item => {
                item.RuleFor(i => i.Quantity).SetValidator(new ItemQuantityValidator()).WithMessage("quantity must to be great than zero");
            });
    }
}
