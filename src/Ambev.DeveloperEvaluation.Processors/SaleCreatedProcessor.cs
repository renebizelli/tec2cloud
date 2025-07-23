using Ambev.DeveloperEvaluation.Domain.Events;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.Processors;

public class SaleCreatedProcessor : IHandleMessages<SaleCreatedEvent> 
{
    public Task Handle(SaleCreatedEvent message)
    {
        Console.WriteLine("****************************************************");
        Console.WriteLine("SALE CREATED");
        Console.WriteLine("****************************************************");
        Console.WriteLine(message.SaleId.ToString());
        Console.WriteLine("****************************************************");

        return Task.CompletedTask;
    }
 
}
