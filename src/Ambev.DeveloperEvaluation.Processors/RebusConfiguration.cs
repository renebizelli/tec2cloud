using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Rebus.Transport.InMem;

namespace Ambev.DeveloperEvaluation.Processors;
public static class RebusConfiguration
{
    public static void AddProcessors(this IServiceCollection services)
    {
        services.AddRebus(configure =>
        {
            configure
                .Transport(t => t.UseInMemoryTransport(new InMemNetwork(), "sales"))
                .Routing(r => r.TypeBased()
                .Map<SaleCreatedEvent>("sales")
                .Map<SaleCancelledEvent>("sales")
                .Map<SaleItemCancelledEvent>("sales"));


        return configure;
        });

        services.AutoRegisterHandlersFromAssemblyOf<SaleCreatedProcessor>();
        services.AutoRegisterHandlersFromAssemblyOf<SaleCancelledProcessor>();
        services.AutoRegisterHandlersFromAssemblyOf<SaleItemCancelledProcessor>();
    }
}
