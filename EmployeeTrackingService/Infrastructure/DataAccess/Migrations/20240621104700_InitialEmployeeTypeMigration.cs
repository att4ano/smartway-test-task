using FluentMigrator;

namespace Infrastructure.DataAccess.Migrations;

[Migration(20240621104700, TransactionBehavior.None)]
public class InitialEmployeeTypeMigration : Migration
{
    public override void Up()
    {
        const string query = @"
DO $$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'employee_type') THEN
            CREATE TYPE employee_type AS
            (
                name VARCHAR(255),
                surname VARCHAR(255),
                phone VARCHAR(20),
                company_id INTEGER,
                passport_id INTEGER,
                department_id INTEGER
            );
        END IF;
    END
$$;
";
        Execute.Sql(query);
    }

    public override void Down()
    {
        const string query = @"
DO $$
    BEGIN
        DROP TYPE IF EXISTS employee_type;
    END
$$;
";
        Execute.Sql(query);
    }
}
