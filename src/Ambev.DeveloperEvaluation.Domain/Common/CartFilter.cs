using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Common;

public class CartFilter
{
    public Guid UserId { get; set; }
    public Guid BranchId { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new CartFilterValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

}
