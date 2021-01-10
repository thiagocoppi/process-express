using Dapper;
using Infraestrutura.Context;
using Infraestrutura.Models.ContaCorrente;
using Infraestrutura.Models.Paginacao;
using System;
using System.Collections.Generic;
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

        public async Task<IList<ContaCorrenteDto>> BuscarTodasContasCorrentesPaginado(PaginacaoDto paginacao)
        {
            var contas = await _processExpressContext.GetConnection().QueryAsync<ContaCorrenteDto, BancoDto, ContaCorrenteDto>(SQL_GERAR_RELATORIO_TRANSACOES_TODAS_CONTAS, param: new
            {
                tamanhoPagina = paginacao.TamanhoPagina,
                pularRegistros = paginacao.PularRegistros
            },
                map: (contaCorrente, banco) =>
            {
                return new ContaCorrenteDto()
                {
                    Agencia = contaCorrente.Agencia,
                    Contato = contaCorrente.Contato,
                    DataNascimento = contaCorrente.DataNascimento,
                    Nome = contaCorrente.Nome,
                    Numero = contaCorrente.Numero,
                    Id = contaCorrente.Id,
                    Banco = new BancoDto()
                    {
                        Codigo = banco.Codigo,
                        Nome = banco.Nome
                    }
                };
            }, splitOn: "Codigo");
            return contas?.ToList();
        }

        public async Task<int> BuscarTotalContasCorrentes()
        {
            return await _processExpressContext.GetConnection().QueryFirstAsync<int>(SQL_CONTAR_QUANTIDADE_CONTAS_CORRENTES);
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

        private const string SQL_GERAR_RELATORIO_TRANSACOES_TODAS_CONTAS =
        @"select ct.nome as Nome,
                 ct.data_nascimento as dataNascimento,
                 ct.contato_principal as Contato,
                 ct.agencia as Agencia,
                 ct.numero as Numero,
                 ct.id as Id,
                 bt.codigo as Codigo,
                 bt.nome as Nome
	            from CONTA ct
            INNER JOIN BANCO as bt ON CT.banco_id = bt.id
            LIMIT :tamanhoPagina
            OFFSET :pularRegistros";

        private const string SQL_CONTAR_QUANTIDADE_CONTAS_CORRENTES =
            @"SELECT COUNT(*)
             FROM CONTA";
    }
}