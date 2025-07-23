using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.Results;

namespace Ambev.DeveloperEvaluation.Domain.Common;

public class CartFilter
{
    public Guid UserId { get; set; }
    public Guid BranchId { get; set; }

    public async Task<ValidationResult> ValidateAsync(CancellationToken cancellationToken)
    {
        var validator = new CartFilterValidator();
        return await validator.ValidateAsync(this, cancellationToken);
    }
}
