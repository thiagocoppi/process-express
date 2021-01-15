using FluentMigrator;

namespace Infraestrutura.Migrations
{
    [Migration(03)]
    [Tags("Production", "Development")]
    //Devido ao SQLite e o FluentMigrator não conseguirem trabalhar com Guids foi necessário separar os default values apenas para o contexto de aplicação
    public class DefaultValues : BaseMigration
    {
        public override void Down()
        {
            Alter.Table("banco").AlterColumn("id").AsGuid().NotNullable();
            Alter.Table("conta").AlterColumn("id").AsGuid().NotNullable();
            Alter.Table("transacao").AlterColumn("id").AsGuid().NotNullable();
        }

        public override void Up()
        {
            Alter.Table("banco").AlterColumn("id").AsGuid().NotNullable().WithDefault(SystemMethods.NewGuid);
            Alter.Table("conta").AlterColumn("id").AsGuid().NotNullable().WithDefault(SystemMethods.NewGuid);
            Alter.Table("transacao").AlterColumn("id").AsGuid().NotNullable().WithDefault(SystemMethods.NewGuid);
        }
    }
}