using Dapper;
using Infraestrutura.Context;
using Infraestrutura.Models.Contabilidade.RelatorioTransacoes;
using Infraestrutura.Models.ContaCorrente;
using Infraestrutura.Models.Paginacao;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infraestrutura.QueryStore
{
    public sealed class ContabilidadeStore : IContabilidadeStore
    {
        private readonly IProcessExpressContext _context;
        private readonly IContaCorrenteStore _contaCorrenteStore;

        public ContabilidadeStore(IProcessExpressContext context, IContaCorrenteStore contaCorrenteStore)
        {
            _context = context;
            _contaCorrenteStore = contaCorrenteStore;
        }

        public async Task<IList<RelatorioTransacoesDto>> GerarRelatorioContabilTodasContas(PaginacaoDto paginacao)
        {
            var listaContasCorrentes = await _contaCorrenteStore.BuscarTodasContasCorrentesPaginado(paginacao);
            var listaRelatorio = new List<RelatorioTransacoesDto>();
            foreach(var contaCorrente in listaContasCorrentes)
            {
                var listaTransacoes = await _context.GetConnection().QueryAsync<TransacaoDto>(SQL_GERAR_RELATORIO_TRANSACOES, param: new 
                {
                    conta_id = contaCorrente.Id
                });

                listaRelatorio.Add(new RelatorioTransacoesDto()
                {
                    ContaCorrente = contaCorrente,
                    TransacoesOcorridas = listaTransacoes.ToList()
                });

            }


            return listaRelatorio;
        }

        private const string SQL_GERAR_RELATORIO_TRANSACOES =
            @"select t.codigo_referencia as NumeroReferencia,
                     t.data_transacao as DataTransacao,
                     t.descricao as Descricao,
                     t.hash as Hash,
                     t.identificador_transacao as IdentificadorTransacao,
                     t.protocolo as Protocolo,
                     t.tipo_transacao as TipoTransacao,
                     t.valor as Valor
	            from transacao t
	            inner join CONTA as ct on T.conta_id = ct.id
	            inner join BANCO as bt on bt.id  = ct.banco_id
                where t.conta_id = :conta_id";
    }
}