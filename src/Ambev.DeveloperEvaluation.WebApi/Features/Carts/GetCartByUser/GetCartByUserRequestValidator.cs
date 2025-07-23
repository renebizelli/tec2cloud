using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartByUser;

public class GetCartByUserRequestValidator : AbstractValidator<GetCartByUserRequest>
{
    public GetCartByUserRequestValidator()
    {
        RuleFor(f => f.UserId).NotEqual(Guid.Empty).WithMessage("Invalid UserId");
        RuleFor(f => f.BranchId).NotEqual(Guid.Empty).WithMessage("Invalid BranchId");
    }
}
