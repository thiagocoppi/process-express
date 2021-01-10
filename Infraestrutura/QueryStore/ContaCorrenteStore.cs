using Dapper;
using Infraestrutura.Context;
using Infraestrutura.Models.ContaCorrente;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infraestrutura.QueryStore
{
    public sealed class ContaCorrenteStore : IContaCorrenteStore
    {
        private readonly IProcessExpressContext _processExpressContext;

        public ContaCorrenteStore(IProcessExpressContext processExpressContext)
        {
            _processExpressContext = processExpressContext;
        }

        public async Task<ContaCorrenteDto> BuscarContaCorrentePeloId(Guid id)
        {
            var result = await _processExpressContext.GetConnection().QueryAsync<ContaCorrenteDto, BancoDto, ContaCorrenteDto>(SQL_BUSCAR_CONTA_PELO_ID, param: new
            {
                idConta = id
            }, map: (conta, banco) =>
            {
                return new ContaCorrenteDto()
                {
                    Agencia = conta.Agencia,
                    Banco = banco,
                    Id = conta.Id,
                    Numero = conta.Numero
                };
            }, splitOn: "BancoId");


            return result.FirstOrDefault();
        }

        private const string SQL_BUSCAR_CONTA_PELO_ID =
            @"SELECT CONTA.ID, 
                     CONTA.AGENCIA, 
                     CONTA.NUMERO, 
                     BANCO.ID BancoId,
                     BANCO.NOME, 
                     BANCO.CODIGO
             FROM CONTA 
               INNER JOIN BANCO ON CONTA.BANCO_ID = BANCO.ID
             WHERE CONTA.ID = :idConta";
    }
}