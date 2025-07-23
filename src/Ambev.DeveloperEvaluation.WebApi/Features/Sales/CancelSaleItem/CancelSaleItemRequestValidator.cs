using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleItem;

public class CancelSaleItemRequestValidator : AbstractValidator<CancelSaleItemRequest>
{
    public CancelSaleItemRequestValidator()
    {
        RuleFor(f => f.SaleId).NotEqual(0).WithMessage("SaleId must be great than zero");
        RuleFor(f => f.SaleItemId).NotEqual(0).WithMessage("SaleItemId must be great than zero");
    }
}
