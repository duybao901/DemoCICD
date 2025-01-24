using DemoCICD.Application.Behaviors;
using DemoCICD.Application.Mapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DemoCICD.Application.DependencyInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfigureMediatR(this IServiceCollection services)
     => services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly))
        // .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationDefaultBehavior<,>)) // !TEST: to compare with ValidationPipelineBehavior
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>)) // 1. Validate Request
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionPipelineBehavior<,>)) // 2. Global Transaction
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformancePipelineBehavior<,>)) // 3. Measure Permance
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(TracingPipelineBehavior<,>)) // 4. Log/Tracing Request
        .AddValidatorsFromAssembly(Contract.AssemblyReference.assembly, includeInternalTypes: true);

    public static IServiceCollection AddConfigureAutoMapper(this IServiceCollection services)
        => services.AddAutoMapper(typeof(ServiceProfile));
}
