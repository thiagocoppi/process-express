using FluentMigrator;

namespace Infraestrutura.Migrations
{
    [Migration(02)]
    [Tags("Production", "Development")]
    public class TransacaoMigration : BaseMigration
    {
        public override void Down()
        {
            Delete.Table("banco");
            Delete.Table("conta");
            Delete.Table("transacao");
        }

        public override void Up()
        {
            Create.Table("banco").WithDescription("Tabela que armazena todos os bancos fornecidos pelo banco central")
                   .WithColumn("id").AsGuid().NotNullable().WithDefault(SystemMethods.NewGuid).NotNullable().PrimaryKey().WithColumnDescription("Identificador único da tabela")
                   .WithColumn("nome").AsString().WithColumnDescription("Nome do banco")
                   .WithColumn("codigo").AsInt32().NotNullable().WithColumnDescription("Código único do banco dentro do BC");

            Create.Table("conta").WithDescription("Tabela que armazena as contas dos clientes")
                .WithColumn("numero").AsInt32().NotNullable().WithColumnDescription("Número da conta")
                .WithColumn("agencia").AsInt32().NotNullable().WithColumnDescription("Código da agência da conta")
                .WithColumn("nome").AsString().Nullable().WithColumnDescription("Nome do titular da conta")
                .WithColumn("data_nascimento").AsDate().Nullable().WithColumnDescription("Data nascimento do titular")
                .WithColumn("contato_principal").AsString().Nullable().WithColumnDescription("Contato principal do titular")
                .WithColumn("id").AsGuid().NotNullable().WithDefault(SystemMethods.NewGuid).PrimaryKey().WithColumnDescription("Identificador único da tabela")
                .WithColumn("banco_id").AsGuid().NotNullable().WithDefault(SystemMethods.NewGuid).NotNullable().ForeignKey("banco", "id");

            Create.Table("transacao").WithDescription("Tabela que armazena todas as transações")
                .WithColumn("id").AsGuid().NotNullable().WithDefault(SystemMethods.NewGuid).PrimaryKey().WithColumnDescription("Identificador único do registro")
                .WithColumn("conta_id").AsGuid().NotNullable().WithDefault(SystemMethods.NewGuid).ForeignKey("conta", "id").WithColumnDescription("Conta vinculada a transação")
                .WithColumn("tipo_transacao").AsString().NotNullable().WithDefaultValue("OUTROS").WithColumnDescription("Tipo da transação")
                .WithColumn("data_transacao").AsDate().NotNullable().WithColumnDescription("Data da transação")
                .WithColumn("valor").AsDecimal().NotNullable().WithColumnDescription("Valor da transação")
                .WithColumn("protocolo").AsString().NotNullable().WithColumnDescription("Protocolo da transação")
                .WithColumn("codigo_referencia").AsString().NotNullable().WithColumnDescription("Código de referência da transação")
                .WithColumn("hash").AsString().NotNullable().WithColumnDescription("Hash que identifica a transação")
                .WithColumn("identificador_transacao").AsInt64().NotNullable().Unique().WithColumnDescription("Identificador único da transação")
                .WithColumn("descricao").AsString().NotNullable().WithColumnDescription("Descrição da transação");
        }
    }
}