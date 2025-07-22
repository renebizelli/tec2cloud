using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;

public static class CreateProductHandlerTestData
{
    private static readonly Faker<CreateProductCommand> createProductHandlerFaker = new Faker<CreateProductCommand>()
    .RuleFor(u => u.Title, f => f.Commerce.ProductName())
    .RuleFor(u => u.Description, f => f.Commerce.ProductDescription())
    .RuleFor(u => u.Price, f => decimal.Parse(f.Commerce.Price()))
    .RuleFor(u => u.Category, f => f.Commerce.Categories(1).First())
    .RuleFor(u => u.Image, f => f.Image.PlaceImgUrl());
    
    public static CreateProductCommand GenerateValidCommand() => createProductHandlerFaker.Generate();
}
