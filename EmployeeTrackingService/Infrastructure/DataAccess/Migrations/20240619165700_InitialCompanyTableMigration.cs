using FluentMigrator;

namespace Infrastructure.DataAccess.Migrations;

[Migration(20240619165700, TransactionBehavior.None)]
public class InitialCompanyTableMigration : Migration
{
    public override void Up()
    {
        const string query = @"
                               CREATE TABLE IF NOT EXISTS company (
                                 id SERIAL PRIMARY KEY,
                                 name VARCHAR(255) NOT NULL
                               );
                               ";
        Execute.Sql(query);
    }

    public override void Down()
    {
        const string query = @"DROP TABLE company";
        Execute.Sql(query);
    }
}