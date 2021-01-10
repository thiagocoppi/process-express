using Domain.Transacoes;
using MediatR;
using System;

namespace Application.Commands.ContaCorrente.Lancamento
{
    public sealed class RealizarLancamentoFinanceiroCommand : IRequest<RealizarLancamentoFinanceiroCommandResult>
    {
        public ContaCommand ContaCorrente { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public IdentificadorFiscoCommand IdentificadorTransacao { get; set; }
        public ETipoTransacao TipoTransacao { get; set; }
    }

    public sealed class ContaCommand
    {
        public BancoCommand Banco { get; set; }
        public long Numero { get; set; }
        public long Agencia { get; set; }
    }

    public sealed class BancoCommand
    {
        public int Codigo { get; set; }
    }

    public sealed class IdentificadorFiscoCommand
    {
        public long IdentificadorUnicoTransacao { get; set; }
        public string Protocolo { get; set; }
        public string NumeroReferencia { get; set; }
        public string Descricao { get; set; }
    }
}