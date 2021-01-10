using Infraestrutura.Models.Contabilidade.RelatorioTransacoes;
using Infraestrutura.Models.Paginacao;
using Infraestrutura.QueryStore;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.Contabilidade.RelatorioTransacoes
{
    public sealed class GerarRelatorioTransacoesContasQueryHandler : IRequestHandler<GerarRelatorioTransacoesContasQueryRequest, GerarRelatorioTransacoesContasQueryResult>
    {
        private readonly IContabilidadeStore _contabilidadeStore;
        private readonly IContaCorrenteStore _contaCorrenteStore;

        public GerarRelatorioTransacoesContasQueryHandler(IContabilidadeStore contabilidadeStore, IContaCorrenteStore contaCorrenteStore)
        {
            _contabilidadeStore = contabilidadeStore;
            _contaCorrenteStore = contaCorrenteStore;
        }

        public async Task<GerarRelatorioTransacoesContasQueryResult> Handle(GerarRelatorioTransacoesContasQueryRequest request, CancellationToken cancellationToken)
        {
            var listaRelatorio = await _contabilidadeStore.GerarRelatorioContabilTodasContas(new PaginacaoDto() { PularRegistros = (request.PageSize * request.PageNumber), TamanhoPagina = request.PageSize });
            var quantidadeContas = await _contaCorrenteStore.BuscarTotalContasCorrentes();
            var retorno = new GerarRelatorioTransacoesContasQueryResult
            {
                TransacoesClientes = new PagedList<RelatorioTransacoesDto>(listaRelatorio, quantidadeContas, request.PageNumber, request.PageSize)
            };

            return retorno;
        }
    }
}