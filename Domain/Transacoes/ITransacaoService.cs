using Domain.Base;
using System.Threading.Tasks;

namespace Domain.Transacoes
{
    public interface ITransacaoService : IDomainService
    {
        Task<Transacao> RealizarLancamento(Transacao transacao);
    }
}