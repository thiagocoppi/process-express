using Domain.Base;
using System.Threading.Tasks;

namespace Domain.Transacoes
{
    public interface ITransacaoStore : IStore
    {
        Task SalvarTransacao(Transacao transacao);
    }
}