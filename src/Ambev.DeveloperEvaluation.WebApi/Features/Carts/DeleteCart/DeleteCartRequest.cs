﻿namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;

public class DeleteCartRequest
{
    public Guid UserId { get; set; }
    public Guid BranchId { get; set; }
}
