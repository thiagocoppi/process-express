﻿using FluentMigrator;

namespace Infraestrutura.Migrations
{
    [Migration(01)]
    public class InitialMigration : BaseMigration
    {
        public override void Down()
        {
            Delete.Table("banco");
            Delete.Table("conta");
        }

        public override void Up()
        {
            Execute.Sql("CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\";");
            Create.Table("banco").WithDescription("Tabela que armazena todos os bancos fornecidos pelo banco central")
                .WithColumn("id").AsGuid().NotNullable().WithDefault(SystemMethods.NewGuid).PrimaryKey().WithColumnDescription("Identificador único da tabela")
                .WithColumn("nome").AsString().WithColumnDescription("Nome do banco")
                .WithColumn("codigo").AsInt32().NotNullable().WithColumnDescription("Código único do banco dentro do BC");

            Create.Table("conta").WithDescription("Tabela que armazena as contas dos clientes")
                .WithColumn("numero").AsInt32().NotNullable().WithColumnDescription("Número da conta")
                .WithColumn("agencia").AsInt32().NotNullable().WithColumnDescription("Código da agência da conta")
                .WithColumn("nome").AsString().Nullable().WithColumnDescription("Nome do titular da conta")
                .WithColumn("data_nascimento").AsDate().Nullable().WithColumnDescription("Data nascimento do titular")
                .WithColumn("contato_principal").AsString().Nullable().WithColumnDescription("Contato principal do titular")
                .WithColumn("id").AsGuid().NotNullable().WithDefault(SystemMethods.NewGuid).PrimaryKey().WithColumnDescription("Identificador único da tabela")
                .WithColumn("banco_id").AsGuid().NotNullable().ForeignKey("banco", "id");
        }
    }
}