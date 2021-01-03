using Domain.Base;
using System.Threading.Tasks;

namespace Domain.Transacoes
{
    public interface IContaStore : IStore
    {
        Task<Conta> RegistrarConta(Conta conta);
    }
}