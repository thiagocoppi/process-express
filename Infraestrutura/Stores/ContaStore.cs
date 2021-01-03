using Dapper;
using Domain.Transacoes;
using Infraestrutura.Context;
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

        public async Task RegistrarConta(Conta conta)
        {
            await _processExpressContext.GetConnection().QueryFirstOrDefaultAsync<Conta>(SQL_VERIFICAR_CONTA_CADASTRADA, new
            {
                numero = conta.Numero,
                agencia = conta.Agencia
            });
        }

        private const string SQL_VERIFICAR_CONTA_CADASTRADA =
            @"INSERT INTO CONTA (NUMERO,AGENCIA,BANCO_ID)
                SELECT :numero, :agencia WHERE 
                    NOT EXISTS (SELECT id FROM CONTA WHERE NUMERO = :numero AND AGENCIA = :agencia) 
                RETURNING id";
    }
}