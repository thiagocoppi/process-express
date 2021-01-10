using FluentMigrator;

namespace Infraestrutura.Migrations
{
    [Migration(02)]
    public class TransacaoMigration : BaseMigration
    {
        public override void Down()
        {
            Delete.Table("transacao");
        }

        public override void Up()
        {
            Create.Table("transacao").WithDescription("Tabela que armazena todas as transações")
                .WithColumn("id").AsGuid().NotNullable().WithDefault(SystemMethods.NewGuid).PrimaryKey().WithColumnDescription("Identificador único do registro")
                .WithColumn("conta_id").AsGuid().NotNullable().ForeignKey("conta", "id").WithColumnDescription("Conta vinculada a transação")
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