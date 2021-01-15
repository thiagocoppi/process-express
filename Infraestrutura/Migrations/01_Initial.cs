using FluentMigrator;

namespace Infraestrutura.Migrations
{
    [Migration(01)]
    [Tags("Production", "Development")]
    public class InitialMigration : BaseMigration
    {
        public override void Down()
        {

        }

        public override void Up()
        {
            Execute.Sql("CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\";");
        }
    }
}