using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;

public static class DeleteProductHandlerTestData
{
    private static readonly Faker<DeleteProductCommand> deleteProductCommandFaker = new Faker<DeleteProductCommand>()
    .RuleFor(u => u.Id, f => f.Random.Int(1));

    public static DeleteProductCommand GenerateValidCommand() => deleteProductCommandFaker.Generate();
    public static DeleteProductCommand GenerateInvalidCommand() => new DeleteProductCommand() { Id = 0 };
}
