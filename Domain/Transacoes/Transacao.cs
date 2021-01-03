using Domain.Base;
using System;

namespace Domain.Transacoes
{
    public sealed class Transacao : BaseEntity
    {
        public Conta Conta { get; private set; }
        public ETipoTransacao TipoTransacao { get; private set; }
        public DateTime DataTransacao { get; private set; }
        public decimal Valor { get; private set; }
        public string Protocolo { get; private set; }
        public string NumeroReferencia { get; private set; }
        public string Descricao { get; private set; }
        public long IdentificadorTransacao { get; private set; }

        public Transacao(Conta conta, ETipoTransacao tipoTransacao, DateTime dataTransacao, decimal valor, string protocolo, string numeroReferencia, string descricao)
        {
            Conta = conta;
            TipoTransacao = tipoTransacao;
            DataTransacao = dataTransacao;
            Valor = valor;
            Protocolo = protocolo;
            NumeroReferencia = numeroReferencia;
            Descricao = descricao;
        }

        public Transacao(Conta conta, ETipoTransacao tipoTransacao, DateTime dataTransacao, decimal valor, string checkNum, string numeroReferencia, string descricao, long identificadorTransacao) : this(conta, tipoTransacao, dataTransacao, valor, checkNum, numeroReferencia, descricao)
        {
            IdentificadorTransacao = identificadorTransacao;
        }
    }
}