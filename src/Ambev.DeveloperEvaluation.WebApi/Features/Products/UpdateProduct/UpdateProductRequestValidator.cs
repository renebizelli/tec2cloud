using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(product => product.Id).NotEqual(0).WithMessage("Product ID is required");
        RuleFor(product => product.Title).NotEmpty().Length(10, 128);
        RuleFor(product => product.Description).NotEmpty();
        RuleFor(product => product.Category).NotEmpty().Length(2, 64);
        RuleFor(product => product.Price).NotEqual(0);
        RuleFor(product => product.Image).NotEmpty();
    }
}
