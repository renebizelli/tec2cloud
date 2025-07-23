using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;

public class DeleteCartRequestValidator : AbstractValidator<DeleteCartRequest>
{
    public DeleteCartRequestValidator()
    {
        RuleFor(f => f.UserId).NotEqual(Guid.Empty).WithMessage("Invalid UserId");
        RuleFor(f => f.BranchId).NotEqual(Guid.Empty).WithMessage("Invalid BranchId");
    }
}
