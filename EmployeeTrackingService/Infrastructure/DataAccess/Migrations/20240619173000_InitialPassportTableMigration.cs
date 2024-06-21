using FluentMigrator;

namespace Infrastructure.DataAccess.Migrations;

[Migration(20240619173000, TransactionBehavior.None)]
public class InitialPassportTableMigration : Migration
{
    public override void Up()
    {
        const string query = @"
                              CREATE TABLE IF NOT EXISTS passport (
                                id SERIAL PRIMARY KEY,
                                type VARCHAR(50),
                                number VARCHAR(50)
                              );
                              ";
        Execute.Sql(query);
    }

    public override void Down()
    {
        const string query = @"DROP TABLE passport";
        Execute.Sql(query);
    }
}