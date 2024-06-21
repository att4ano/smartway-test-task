using FluentMigrator;

namespace Infrastructure.DataAccess.Migrations;

[Migration(20240619173200, TransactionBehavior.None)]
public class InitialEmployeeTableMigration : Migration
{
    public override void Up()
    {
        const string query = @"
                                CREATE TABLE IF NOT EXISTS employee (
                                  id SERIAL PRIMARY KEY,
                                  name VARCHAR(255),
                                  surname VARCHAR(255),
                                  phone VARCHAR(20),
                                  company_id INTEGER,
                                  passport_id INTEGER,
                                  department_id INTEGER,
                                  FOREIGN KEY (company_id) REFERENCES company(id),
                                  FOREIGN KEY (passport_id) REFERENCES passport(id),
                                  FOREIGN KEY (department_id) REFERENCES department(id)
                                );
                              ";
        Execute.Sql(query);
    }

    public override void Down()
    {
        const string query = @"DROP TABLE employee";
        Execute.Sql(query);
    }
}