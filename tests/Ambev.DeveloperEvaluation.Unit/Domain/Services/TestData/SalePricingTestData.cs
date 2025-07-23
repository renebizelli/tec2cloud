using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Services.TestData;

public static class SalePricingTestData
{
    private static readonly Faker<Product> productFaker = new Faker<Product>()
    .RuleFor(u => u.Id, f => f.Random.Int(0, 1000))
    .RuleFor(u => u.Title, f => f.Commerce.ProductName())
    .RuleFor(u => u.Description, f => f.Commerce.ProductDescription())
    .RuleFor(u => u.Price, f => decimal.Parse(f.Commerce.Price()))
    .RuleFor(u => u.Category, f => f.Commerce.Categories(1).First())
    .RuleFor(u => u.Active, f => true)
    .RuleFor(u => u.Image, f => f.Image.PlaceImgUrl());

    public static Product GenerateValidProduct() => productFaker.Generate();
}
