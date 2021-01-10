using Domain.Base;
using Infraestrutura.Models.Contabilidade.RelatorioTransacoes;
using Infraestrutura.Models.Paginacao;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infraestrutura.QueryStore
{
    public interface IContabilidadeStore : IStore
    {
        Task<IList<RelatorioTransacoesDto>> GerarRelatorioContabilTodasContas(PaginacaoDto paginacao);
    }
}