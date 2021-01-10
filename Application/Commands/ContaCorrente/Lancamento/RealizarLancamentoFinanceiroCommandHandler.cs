using Domain.Transacoes;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.ContaCorrente.Lancamento
{
    public sealed class RealizarLancamentoFinanceiroCommandHandler : IRequestHandler<RealizarLancamentoFinanceiroCommand, RealizarLancamentoFinanceiroCommandResult>
    {
        private readonly ITransacaoService _transacaoService;

        public RealizarLancamentoFinanceiroCommandHandler(ITransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }

        public async Task<RealizarLancamentoFinanceiroCommandResult> Handle(RealizarLancamentoFinanceiroCommand request, CancellationToken cancellationToken)
        {
            var transacao = new Transacao(new Conta(new Banco(string.Empty, request.ContaCorrente.Banco.Codigo),
                request.ContaCorrente.Numero, request.ContaCorrente.Agencia), request.TipoTransacao, request.Data, request.Valor,
                request.IdentificadorTransacao?.Protocolo, request.IdentificadorTransacao?.NumeroReferencia, request.IdentificadorTransacao?.Descricao);

            transacao.AlterarIdentificadorUnicoTransacao(request.IdentificadorTransacao?.IdentificadorUnicoTransacao);

            var transacaoCadastrada = await _transacaoService.RealizarLancamento(transacao);

            var lancamentoManualFinanceiroCommandResult = new RealizarLancamentoFinanceiroCommandResult()
            {
                Identificador = transacaoCadastrada.Id                
            };

            foreach (var alerta in transacaoCadastrada.Alerta)
            {
                lancamentoManualFinanceiroCommandResult.Alertas.Add(new AdevertenciaLancamentoCommand()
                {
                    Codigo = alerta.Codigo,
                    Mensagem = alerta.Mensagem,
                    Nivel = alerta.Nivel.ToString()
                });
            }
            return lancamentoManualFinanceiroCommandResult;
        }
    }
}