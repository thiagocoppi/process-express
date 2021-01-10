using Dapper;
using Domain.Transacoes;
using Infraestrutura.Context;
using System;
using System.Threading.Tasks;

namespace Infraestrutura.Stores
{
    public sealed class ContaStore : IContaStore
    {
        private readonly IProcessExpressContext _processExpressContext;

        public ContaStore(IProcessExpressContext processExpressContext)
        {
            _processExpressContext = processExpressContext;
        }

        public async Task<Conta> RegistrarConta(Conta conta)
        {
            var id = await _processExpressContext.GetConnection().ExecuteScalarAsync<Guid>(SQL_VERIFICAR_CONTA_CADASTRADA, new
            {
                numero = conta.Numero,
                agencia = conta.Agencia,
                banco_id = conta.Banco.Id
            });

            conta.AlterarIdentificador(id);

            return conta;
        }

        public async Task<Guid> BuscarIdentificadorConta(Conta conta)
        {
            return await _processExpressContext.GetConnection().QueryFirstOrDefaultAsync<Guid>(SQL_BUSCAR_ID_CONTA, new
            {
                numero = conta.Numero,
                agencia = conta.Agencia,
                banco_id = conta.Banco.Id
            });
        }

        public async Task<Conta> BuscarConta(Conta conta)
        {
            return await _processExpressContext.GetConnection().QueryFirstOrDefaultAsync<Conta>(SQL_VERIFICAR_EXISTE, new
            {
                numero = conta.Numero,
                agencia = conta.Agencia
            });
        }

        public async Task AtualizarDadosCadastrais(Conta conta)
        {
            await _processExpressContext.GetConnection().ExecuteAsync(SQL_ATUALIZAR_DADOS_CADASTRAIS, new
            {
                nomeTitular = conta.NomeTitular,
                dtNascimento = conta.DataNascimento,
                contato = conta.TelefoneContato,
                id = conta.Id
            });
        }

        private const string SQL_VERIFICAR_CONTA_CADASTRADA =
            @"INSERT INTO CONTA (NUMERO,AGENCIA,BANCO_ID)
                SELECT :numero, :agencia, :banco_id WHERE
                    NOT EXISTS (SELECT id FROM CONTA WHERE NUMERO = :numero AND AGENCIA = :agencia AND BANCO_ID = :banco_id)
                RETURNING id";

        private const string SQL_BUSCAR_ID_CONTA =
            @"SELECT id FROM CONTA WHERE NUMERO = :numero AND AGENCIA = :agencia AND BANCO_ID = :banco_id";

        private const string SQL_VERIFICAR_EXISTE =
            @"SELECT CONTA.ID, CONTA.NUMERO, CONTA.AGENCIA, CONTA.NOME, CONTA.DATA_NASCIMENTO as DataNascimento FROM CONTA 
                INNER JOIN BANCO ON BANCO.ID = CONTA.BANCO_ID    
                WHERE NUMERO = :numero AND AGENCIA = :agencia";

        private const string SQL_ATUALIZAR_DADOS_CADASTRAIS =
            @"UPDATE CONTA SET NOME = :nomeTitular, DATA_NASCIMENTO = :dtNascimento, CONTATO_PRINCIPAL = :contato WHERE ID = :id";
    }
}