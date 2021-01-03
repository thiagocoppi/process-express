using Domain.Base;
using System;
using System.Threading.Tasks;

namespace Domain.Transacoes
{
    public interface IContaStore : IStore
    {
        Task<Conta> RegistrarConta(Conta conta);
        Task<Guid> BuscarIdentificadorConta(Conta conta);
    }
}