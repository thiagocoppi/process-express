using MediatR;

namespace Application.Queries.Contabilidade.RelatorioTransacoes
{
    public sealed class GerarRelatorioTransacoesContasQueryRequest : IRequest<GerarRelatorioTransacoesContasQueryResult>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}