using DemoCICD.Application.DependencyInjection.Extensions;
using DemoCICD.Persistence.DependencyInjection.Extensions;
using DemoCICD.API.DependencyInjection.Extensions;
using DemoCICD.Persistence.DependencyInjection.Options;
using Serilog;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using DemoCICD.API.Middleware;
using DemoCICD.Infrastructure.Dapper.DependencyInjection.Extensions;
using DemoCICD.Presentation.Endpoints.Products;
using Carter;

var builder = WebApplication.CreateBuilder(args);
// Log
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging.ClearProviders().AddSerilog();
builder.Host.UseSerilog();

// Api
builder.Services.AddControllers().AddApplicationPart(DemoCICD.Presentation.AssemblyReference.Assembly);

// Middleware
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

// Add configuration
builder.Services.AddConfigureMediatR();
builder.Services.ConfigureSqlServerRetryOptions(builder.Configuration.GetSection(nameof(SqlServerRetryOptions)));
builder.Services.AddSqlConfiguration();
builder.Services.AddRepositoryBaseConfiguration();
builder.Services.AddConfigureAutoMapper();

// Api Minimal
builder.Services.AddSingleton<ProductApi>();

// Add Carter
builder.Services.AddCarter();

// Config Dapper
builder.Services.AddInfrastructureDapper();

// Version
builder.Services
        .AddSwaggerGenNewtonsoftSupport()
        .AddFluentValidationRulesToSwagger()
        .AddEndpointsApiExplorer()
        .AddSwagger();

builder.Services
    .AddApiVersioning(options => options.ReportApiVersions = true)
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Add Minial API Endpoint
// app.NewVersionedApi("product-minial-show-on-swagger").MapProductApiV1().MapProductApiV2();
var versionedApi = app.NewVersionedApi();
var apiV1 = app.Services.GetRequiredService<ProductApi>();
apiV1.MapProductApiV1(versionedApi);

// Add API Endpoint with carter module
app.MapCarter();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

if (builder.Environment.IsDevelopment() || builder.Environment.IsStaging())
    app.ConfigureSwagger();

try
{
    await app.RunAsync();
    Log.Information("Stopped cleanly");
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
    await app.StopAsync();
}
finally
{
    Log.CloseAndFlush();
    await app.DisposeAsync();
}
