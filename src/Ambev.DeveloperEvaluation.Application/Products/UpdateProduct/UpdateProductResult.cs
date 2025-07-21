﻿namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

public class UpdateProductResult 
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
    public UpdateProductRatingResult? Rating { get; set; }
}

public class UpdateProductRatingResult
{
    public int Count { get; set; }
    public decimal Rate { get; set; }
}