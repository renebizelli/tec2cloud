using Ambev.DeveloperEvaluation.Domain.Events;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.Processors;

public class SaleItemCancelledProcessor : IHandleMessages<SaleItemCancelledEvent> 
{
    public Task Handle(SaleItemCancelledEvent message)
    {
        Console.WriteLine( "****************************************************");
        Console.WriteLine("SALE ITEM DELETED");
        Console.WriteLine("****************************************************");
        Console.WriteLine( message.SaleId.ToString());
        Console.WriteLine( message.SaleItemId.ToString());
        Console.WriteLine("****************************************************");

        return Task.CompletedTask;
    }
 
}
