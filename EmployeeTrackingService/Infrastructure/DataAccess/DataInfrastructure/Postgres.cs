using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Models.Models;
using Npgsql;
using Npgsql.NameTranslation;
using Npgsql.TypeMapping;

namespace Infrastructure.DataAccess.DataInfrastructure;

public static class Postgres
{
    private static readonly INpgsqlNameTranslator Translator = new NpgsqlSnakeCaseNameTranslator();
    public static NpgsqlDataSource SetupDataSource(string connectionString)
    {
        var builder = new NpgsqlDataSourceBuilder(connectionString);
        MapCompositeTypes(builder);
        return builder.Build();
    }

    private static void MapCompositeTypes(INpgsqlTypeMapper builder)
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        
        builder.MapComposite<Employee>("employee_type", Translator);
    }
    
    public static void AddMigrations(this IServiceCollection collection, string connectionString)
    {
        collection.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb.AddPostgres()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(Postgres).Assembly).For.Migrations()
            )
            .AddLogging(lb => lb.AddFluentMigratorConsole());
    }
}   