using Infraestrutura.Models.Contabilidade.RelatorioTransacoes;

namespace Application.Queries.Contabilidade.RelatorioTransacoes
{
    public sealed class GerarRelatorioTransacoesContasQueryResult
    {
        public GerarRelatorioTransacoesContasQueryResult()
        {
        }

        public PagedList<RelatorioTransacoesDto> TransacoesClientes { get; set; }
    }
}