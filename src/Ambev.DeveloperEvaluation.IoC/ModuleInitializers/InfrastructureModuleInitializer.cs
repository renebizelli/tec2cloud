using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.NoSQL;
using Ambev.DeveloperEvaluation.NoSQL.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class InfrastructureModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        ORMInitialize(builder);

        new MongoDbInitialize().Initialize(builder);

        builder.Services.AddTransient<IUserRepository, UserRepository>();
        builder.Services.AddTransient<IProductRepository, ProductRepository>();
        builder.Services.AddTransient<ICartRepository, CartRepository>();
        builder.Services.AddTransient<ISaleRepository, SaleRepository>();
    }

    private void ORMInitialize(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<DbContext>(provider => provider.GetRequiredService<DefaultContext>());
    }


}