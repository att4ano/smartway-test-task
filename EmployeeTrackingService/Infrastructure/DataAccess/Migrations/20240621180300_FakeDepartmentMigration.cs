using FluentMigrator;

namespace Infrastructure.DataAccess.Migrations;

[Migration(20240621180300, TransactionBehavior.None)]
public class FakeDepartmentMigration : Migration
{
    public override void Up()
    {
        for (int i = 0; i < 10; ++i)
        {
            Insert.IntoTable("department")
                .Row(
                    new
                    {
                        name = Faker.Company.Name(),
                        phone_number = Faker.Phone.Number(),
                        company_id = new Random().Next(1, 10)
                    });            
        }
    }

    public override void Down()
    {
        Delete.FromTable("company").AllRows();
    }
}