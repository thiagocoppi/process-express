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
            return await _processExpressContext.GetConnection().QueryFirstOrDefaultAsync<Guid>(SQL_VERIFICAR_CONTA_EXISTENTE, new
            {
                numero = conta.Numero,
                agencia = conta.Agencia,
                banco_id = conta.Banco.Id
            });
        }

        private const string SQL_VERIFICAR_CONTA_CADASTRADA =
            @"INSERT INTO CONTA (NUMERO,AGENCIA,BANCO_ID)
                SELECT :numero, :agencia, :banco_id WHERE
                    NOT EXISTS (SELECT id FROM CONTA WHERE NUMERO = :numero AND AGENCIA = :agencia AND BANCO_ID = :banco_id)
                RETURNING id";

        private const string SQL_VERIFICAR_CONTA_EXISTENTE =
            @"SELECT id FROM CONTA WHERE NUMERO = :numero AND AGENCIA = :agencia AND BANCO_ID = :banco_id";
    }
}