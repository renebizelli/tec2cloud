using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;


public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(product => product.Id).NotEqual(0);
        RuleFor(product => product.Title).NotEmpty().Length(10, 128);
        RuleFor(product => product.Description).NotEmpty();
        RuleFor(product => product.Category).NotEmpty().Length(2, 64);
        RuleFor(product => product.Price).NotEqual(0);
        RuleFor(product => product.Image).NotEmpty();
    }
}
