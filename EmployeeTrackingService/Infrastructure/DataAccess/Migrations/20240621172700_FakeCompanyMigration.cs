using FluentMigrator;

namespace Infrastructure.DataAccess.Migrations;

[Migration(20240621172700, TransactionBehavior.None)]
public class FakeCompanyMigration : Migration
{
    public override void Up()
    {
        for (int i = 0; i < 10; ++i)
        {
            Insert.IntoTable("company")
                .Row(
                    new
                    {
                        name = Faker.Company.Name()
                    });            
        }

    }

    public override void Down()
    {
        Delete.FromTable("company").AllRows();
    }
}