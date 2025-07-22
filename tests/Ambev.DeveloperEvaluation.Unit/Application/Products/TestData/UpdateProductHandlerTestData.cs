using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;

public static class UpdateProductHandlerTestData
{
    private static readonly Faker<UpdateProductCommand> updateProductHandlerFaker = new Faker<UpdateProductCommand>()
    .RuleFor(u => u.Id, f => f.Random.Int(1))
    .RuleFor(u => u.Title, f => f.Commerce.ProductName())
    .RuleFor(u => u.Description, f => f.Commerce.ProductDescription())
    .RuleFor(u => u.Price, f => decimal.Parse(f.Commerce.Price()))
    .RuleFor(u => u.Category, f => f.Commerce.Categories(1).First())
    .RuleFor(u => u.Image, f => f.Image.PlaceImgUrl());
    
    public static UpdateProductCommand GenerateValidCommand() => updateProductHandlerFaker.Generate();
}
