using Ambev.DeveloperEvaluation.Domain.Events;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.Processors;

public class SaleCancelledProcessor : IHandleMessages<SaleCancelledEvent> 
{
    public Task Handle(SaleCancelledEvent message)
    {
        Console.WriteLine( "****************************************************");
        Console.WriteLine("SALE DELETED");
        Console.WriteLine("****************************************************");
        Console.WriteLine( message.SaleId.ToString());
        Console.WriteLine("****************************************************");

        return Task.CompletedTask;
    }
 
}
