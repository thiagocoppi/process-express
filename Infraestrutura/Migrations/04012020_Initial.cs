using FluentMigrator;
using System;

namespace Infraestrutura.Migrations
{
    [Migration(040120201500)]
    public class InitialMigration : BaseMigration
    {
        public override void Down()
        {
            Delete.Table("banco");
            Delete.Table("conta");
        }

        public override void Up()
        {
            Create.Table("banco").WithDescription("Tabela que armazena todos os bancos fornecidos pelo banco central")
                .WithColumn("id").AsGuid().NotNullable().WithDefaultValue(Guid.NewGuid()).PrimaryKey().WithColumnDescription("Identificador único da tabela")
                .WithColumn("nome").AsInt64().NotNullable().WithColumnDescription("Nome do banco")
                .WithColumn("codigo").AsInt32().NotNullable().WithColumnDescription("Código único do banco dentro do BC");
                
            Create.Table("conta").WithDescription("Tabela que armazena as contas dos clientes")
                .WithColumn("numero").AsInt32().NotNullable().WithColumnDescription("Número da conta")
                .WithColumn("agencia").AsInt32().NotNullable().WithColumnDescription("Código da agência da conta")
                .WithColumn("id").AsGuid().NotNullable().WithDefaultValue(Guid.NewGuid()).PrimaryKey().WithColumnDescription("Identificador único da tabela")
                .WithColumn("banco_id").AsGuid().NotNullable().ForeignKey("banco", "id");
        }
    }
}