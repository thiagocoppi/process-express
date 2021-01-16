using Domain.Exceptions;
using Domain.Transacoes;
using Languages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Domain.ContaCorrentes
{
    public sealed class ContaService : IContaService
    {
        private readonly IContaStore _contaStore;
        private readonly ILogger<ContaService> _logger;
        public ContaService(IContaStore contaStore, ILogger<ContaService> logger)
        {
            _contaStore = contaStore;
            _logger = logger;
        }

        public async Task<Conta> AtualizarDadosCadastrais(Conta contaCorrente)
        {
            if (contaCorrente is null)
            {
                _logger.LogError("Foi informado uma conta nula para atualizar os dados cadastrais!");
                throw new BusinessException(Messages.ContaInvalidaParaAtualizarDados);
            }

            await _contaStore.AtualizarDadosCadastrais(contaCorrente);

            return contaCorrente;
        }
    }
}