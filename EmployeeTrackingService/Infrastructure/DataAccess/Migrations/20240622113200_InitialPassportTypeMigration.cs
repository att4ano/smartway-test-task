using FluentMigrator;

namespace Infrastructure.DataAccess.Migrations;

[Migration(20240622113200, TransactionBehavior.None)]
public class InitialPassportTypeMigration : Migration
{
    public override void Up()
    {
        const string query = """

                             DO $$
                                 BEGIN
                                     IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'passport_type') THEN
                                         CREATE TYPE passport_type AS
                                         (
                                             type VARCHAR(255),
                                             number VARCHAR(255)
                                         );
                                     END IF;
                                 END
                             $$;

                             """;
        Execute.Sql(query);
    }

    public override void Down()
    {
        const string query = """

                             DO $$
                                 BEGIN
                                     DROP TYPE IF EXISTS passport_type;
                                 END
                             $$;
                             ";
                             """;
        Execute.Sql(query);
    }
}