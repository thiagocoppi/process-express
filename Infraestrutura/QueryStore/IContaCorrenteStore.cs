using Domain.Base;
using Infraestrutura.Models.ContaCorrente;
using Infraestrutura.Models.Paginacao;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infraestrutura.QueryStore
{
    public interface IContaCorrenteStore : IStore
    {
        Task<ContaCorrenteDto> BuscarContaCorrentePeloId(Guid id);
        Task<IList<ContaCorrenteDto>> BuscarTodasContasCorrentesPaginado(PaginacaoDto paginacao);
        Task<int> BuscarTotalContasCorrentes();
    }
}