using FluentMigrator;

namespace Infrastructure.DataAccess.Migrations;

[Migration(20240619164200, TransactionBehavior.None)]
public class EmptyMigration : Migration
{
    public override void Up()
    {
    }

    public override void Down()
    {
    }
}