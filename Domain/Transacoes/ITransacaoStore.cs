using Domain.Base;
using System.Threading.Tasks;

namespace Domain.Transacoes
{
    public interface ITransacaoStore : IStore
    {
        Task<Transacao> SalvarTransacaoComIdentificadorUnico(Transacao transacao);

        Task<Transacao> SalvarTransacao(Transacao transacao);

        Task<bool> VerificarHashExiste(string hash);

        Task<bool> VerificarIdentificadorUnicoTransacaoFiscoExiste(long identificador);
    }
}