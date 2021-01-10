using Domain.Exceptions;
using Domain.Transacoes;
using Languages;
using System.Threading.Tasks;

namespace Domain.ContaCorrentes
{
    public sealed class ContaService : IContaService
    {
        private readonly IContaStore _contaStore;

        public ContaService(IContaStore contaStore)
        {
            _contaStore = contaStore;
        }

        public async Task<Conta> AtualizarDadosCadastrais(Conta contaCorrente)
        {
            if (contaCorrente is null)
            {
                throw new BusinessException(Messages.ContaInvalidaParaAtualizarDados);
            }

            await _contaStore.AtualizarDadosCadastrais(contaCorrente);

            return contaCorrente;
        }
    }
}