using DemoCICD.Domain.Abstractions.Dappers;
using DemoCICD.Domain.Abstractions.Dappers.Repositories;
using DemoCICD.Infrastructure.Dapper.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DemoCICD.Infrastructure.Dapper.DependencyInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureDapper(this IServiceCollection services)
    {
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IUnitOfWork, DapperUnitOfWork>();
    }
}
