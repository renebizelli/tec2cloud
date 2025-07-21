using Ambev.DeveloperEvaluation.Domain.Services.Sales.Discounts;
using Ambev.DeveloperEvaluation.Domain.Services.Sales.Pricing;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class DomainModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<ISalePricing, SalePricing>();
        builder.Services.AddTransient<ISaleDiscountApplier, SaleDiscountApplier>();
    }
}