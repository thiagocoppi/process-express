using Domain.Exceptions;
using Languages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Domain.Transacoes
{
    public sealed class TransacaoService : ITransacaoService
    {
        private const string CODIGO_MENSAGEM = "C1";
        private const string MENSAGEM_ALERTA = "Hash encontrado em duplicidade no lançamento manual";
        private readonly ITransacaoStore _transacaoStore;
        private readonly IContaStore _contaStore;
        private readonly ILogger<TransacaoService> _logger;

        public TransacaoService(ITransacaoStore transacaoStore, ILogger<TransacaoService> logger, IContaStore contaStore)
        {
            _transacaoStore = transacaoStore;
            _logger = logger;
            _contaStore = contaStore;
        }

        public async Task<Transacao> RealizarLancamento(Transacao transacao)
        {
            if (transacao is null)
            {
                _logger.LogError("Transação que foi passada é nula");
                throw new BusinessException(Messages.TransacaoInvalida);
            }

            var conta = await _contaStore.BuscarConta(transacao.Conta);

            if (conta is null)
            {
                _logger.LogError("Não foi encontrada a conta {0}/{1}", transacao.Conta.Agencia, transacao.Conta.Numero);
                throw new BusinessException(string.Format(Messages.ContaInexistente, 
                    transacao.Conta.Agencia, transacao.Conta.Numero));
            }

            transacao.AlterarContaCorrente(conta);

            var hashExiste = await _transacaoStore.VerificarHashExiste(transacao.Hash);

            if (hashExiste)
            {
                _logger.LogWarning("Está sendo inserido um lançamento manual cuja o hash {0} já existe cadastrado na base de dados", transacao.Hash);
                transacao.RegistrarAlerta(new Alerta(CODIGO_MENSAGEM, MENSAGEM_ALERTA, ENivelAlerta.GRAVE));
            }

            if (transacao.IdentificadorTransacao != 0)
            {
                var transacaoJaCadastrada = await _transacaoStore.VerificarIdentificadorUnicoTransacaoFiscoExiste(transacao.IdentificadorTransacao);

                if (transacaoJaCadastrada)
                {
                    _logger.LogError("Foi tentado realizar um lançamento duplicado de forma manual, identificador da tentativa do lançamento {0}", transacao.IdentificadorTransacao);
                    throw new BusinessException(string.Format(Messages.LancamentoDuplicado, transacao.IdentificadorTransacao));
                }

                var transacaoInserida = await _transacaoStore.SalvarTransacaoComIdentificadorUnico(transacao);
                transacao.AlterarIdentificador(transacaoInserida.Id);
                return transacao;
            }

            var transacaoInseridaSemIdentificadorUnico = await _transacaoStore.SalvarTransacao(transacao);
            transacao.AlterarIdentificador(transacaoInseridaSemIdentificadorUnico.Id);
            return transacao;
        }
    }
}