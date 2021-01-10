using Domain;
using System;
using System.Collections.Generic;

namespace Application.Commands.ContaCorrente.Lancamento
{
    public sealed class RealizarLancamentoFinanceiroCommandResult
    {
        public Guid Identificador { get; set; }
        public IList<AdevertenciaLancamentoCommand> Alertas { get; set; }

        public RealizarLancamentoFinanceiroCommandResult()
        {
            Alertas = new List<AdevertenciaLancamentoCommand>();
        }
    }

    /// <summary>
    /// Mensagens que advertem os lançamentos manuais, por exemplo, identificado um lancamento duplicado
    /// </summary>
    public sealed class AdevertenciaLancamentoCommand
    {
        public string Mensagem { get; set; }
        public string Codigo { get; set; }
        public string Nivel { get; set; }
    }
}