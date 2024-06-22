using Abstractions;
using FluentMigrator.Runner;
using Infrastructure.DataAccess.DataInfrastructure;
using Infrastructure.DataAccess.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection collection)
    {
        collection.AddScoped<IEmployeeRepository, EmployeeRepository>();
        collection.AddScoped<IPassportRepository, PassportRepository>();
        return collection;
    }

    public static IServiceCollection AddDataInfrastructure(
        this IServiceCollection collection,
        IConfiguration cfg)
    {
        var connectionString =
            cfg.GetConnectionString("PostgresConnectionString") ?? throw new InvalidOperationException();
        
        collection.AddSingleton(Postgres.SetupDataSource(connectionString));
        collection.AddMigrations(connectionString);

        return collection;
    }

    public static WebApplication DoMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
        return app;
    }
    
}