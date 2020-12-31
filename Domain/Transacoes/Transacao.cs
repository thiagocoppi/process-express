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
        public ulong CheckNum { get; private set; }
        public int NumeroReferencia { get; private set; }
        public string Descricao { get; private set; }
        public ulong IdentificadorTransacao { get; private set; }

        public Transacao(Conta conta, ETipoTransacao tipoTransacao, DateTime dataTransacao, decimal valor, ulong checkNum, int numeroReferencia, string descricao)
        {
            Conta = conta;
            TipoTransacao = tipoTransacao;
            DataTransacao = dataTransacao;
            Valor = valor;
            CheckNum = checkNum;
            NumeroReferencia = numeroReferencia;
            Descricao = descricao;
        }

        public Transacao(Conta conta, ETipoTransacao tipoTransacao, DateTime dataTransacao, decimal valor, ulong checkNum, int numeroReferencia, string descricao, ulong identificadorTransacao) : this(conta, tipoTransacao, dataTransacao, valor, checkNum, numeroReferencia, descricao)
        {
            IdentificadorTransacao = identificadorTransacao;
        }
    }
}