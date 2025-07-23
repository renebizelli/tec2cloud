using Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Common;

public class ListRequestValidator : AbstractValidator<ListRequest>
{
    public ListRequestValidator()
    {
        RuleFor(request => request.Page).GreaterThan(0).WithMessage("Page must be great than 0"); ;
        RuleFor(request => request.PageSize).GreaterThan(0).WithMessage("Page size must be great than 0"); ;
    }
}
