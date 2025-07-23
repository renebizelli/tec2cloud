using Ambev.DeveloperEvaluation.WebApi.Common;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;


public class ListSalesRequestValidator : AbstractValidator<ListSalesRequest>
{
    public ListSalesRequestValidator()
    {
        Include(new ListRequestValidator());

        RuleFor(request => request)
            .Must(m =>
                ((m.MinTotalPrice > 0 && m.MaxTotalPrice > 0) && m.MinTotalPrice <= m.MaxTotalPrice) ||
                (m.MinTotalPrice == 0 || m.MaxTotalPrice == 0))
                .WithMessage("must be zero or minTotalPrice <= maxTotalPrice");

        RuleFor(x => x)
            .Must(x =>
                x.MinDate.Equals(DateTime.MinValue) ||
                x.MaxDate.Equals(DateTime.MinValue) ||
                x.MinDate <= x.MaxDate)
            .WithMessage("The min date must be less than or equal to the max date.");
    }
}
