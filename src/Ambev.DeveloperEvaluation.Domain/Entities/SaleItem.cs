﻿namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }

    public int SaleId { get; set; }
    public Sale? Sale { get; set; }

    public decimal TotalPrice { get { return (Price * Quantity) - Discount; } }
}