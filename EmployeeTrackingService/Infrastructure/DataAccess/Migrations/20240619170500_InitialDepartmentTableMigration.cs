using FluentMigrator;

namespace Infrastructure.DataAccess.Migrations;

[Migration(20240619170500, TransactionBehavior.None)]
public class InitialDepartmentTableMigration : Migration
{
    public override void Up()
    {
        const string query = @"
                                CREATE TABLE IF NOT EXISTS department (
                                    id SERIAL PRIMARY KEY,
                                    name VARCHAR(255) NOT NULL,
                                    phone_number VARCHAR(20),
                                    company_id INTEGER NOT NULL,
                                    FOREIGN KEY (company_id) REFERENCES company (id));
                             ";
        Execute.Sql(query);
    }

    public override void Down()
    {
        const string query = @"DROP TABLE department";
        Execute.Sql(query);
    }
}