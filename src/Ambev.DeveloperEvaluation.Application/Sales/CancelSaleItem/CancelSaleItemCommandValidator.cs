using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;

public class CancelSaleItemCommandValidator : AbstractValidator<CancelSaleItemCommand>
{
    public CancelSaleItemCommandValidator()
    {
        RuleFor(f => f.SaleId).NotEqual(0).WithMessage("SaleId must be great than zero");
        RuleFor(f => f.SaleItemId).NotEqual(0).WithMessage("SaleItemId must be great than zero");
    }
}
