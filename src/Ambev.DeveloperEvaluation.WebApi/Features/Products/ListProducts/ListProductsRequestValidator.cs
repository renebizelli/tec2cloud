﻿using Ambev.DeveloperEvaluation.WebApi.Common;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;

public class ListProductsRequestValidator : AbstractValidator<ListProductsRequest>
{
    public ListProductsRequestValidator()
    {
        Include(new ListRequestValidator());

        RuleFor(request => request)
            .Must(m =>
                ((m.MinPrice > 0 && m.MaxPrice > 0) && m.MinPrice <= m.MaxPrice) ||
                (m.MinPrice == 0 || m.MaxPrice == 0))
                .WithMessage("must be zero or minPrice <= maxPrice");
    }
}
