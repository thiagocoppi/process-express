using Domain.Base;
using Domain.Transacoes;
using System.Threading.Tasks;

namespace Domain.ContaCorrentes
{
    public interface IContaService : IDomainService
    {
        Task<Conta> AtualizarDadosCadastrais(Conta contaCorrente);
    }
}