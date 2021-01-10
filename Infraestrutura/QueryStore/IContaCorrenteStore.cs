using Domain.Base;
using Infraestrutura.Models.ContaCorrente;
using System;
using System.Threading.Tasks;

namespace Infraestrutura.QueryStore
{
    public interface IContaCorrenteStore : IStore
    {
        Task<ContaCorrenteDto> BuscarContaCorrentePeloId(Guid id);
    }
}