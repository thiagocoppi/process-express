using Domain.Base;
using Domain.Exceptions;
using Languages;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

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
        public string Hash { get; private set; }
        public IList<Alerta> Alerta { get; private set; }

        public Transacao(Conta conta, ETipoTransacao tipoTransacao, DateTime dataTransacao, decimal valor, string protocolo, string numeroReferencia, string descricao)
        {
            Conta = conta;
            TipoTransacao = tipoTransacao;
            DataTransacao = dataTransacao;
            Valor = valor;
            Protocolo = protocolo;
            NumeroReferencia = numeroReferencia;
            Descricao = descricao;
            Hash = GerarHash(valor, dataTransacao, conta);
        }

        public Transacao(Conta conta, ETipoTransacao tipoTransacao, DateTime dataTransacao, decimal valor, string checkNum, string numeroReferencia, string descricao, long identificadorTransacao) : this(conta, tipoTransacao, dataTransacao, valor, checkNum, numeroReferencia, descricao)
        {
            IdentificadorTransacao = identificadorTransacao;
        }

        public string GerarHash(decimal valor, DateTime data, Conta conta)
        {
            if (conta is null || conta.Banco is null)
            {
                throw new BusinessException(Messages.ErroGerarHashContaInvalida);
            }

            var hashGerar = string.Concat(valor.ToString("#"), data.ToShortDateString(), conta.Numero.ToString(),
                conta.Agencia.ToString(), conta.Banco.Codigo.ToString());

            using MD5 md5 = MD5.Create();
            
            byte[] inputBytes = Encoding.ASCII.GetBytes(hashGerar);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public void RegistrarAlerta(Alerta alerta)
        {
            if (Alerta is null)
            {
                Alerta = new List<Alerta>();
            }

            Alerta.Add(alerta);
        }

        public void AlterarIdentificadorUnicoTransacao(long? identificadorTransacao)
        {
            IdentificadorTransacao = identificadorTransacao ?? 0;
        }

        public void AlterarContaCorrente(Conta conta)
        {
            Conta = conta;
        } 
    }
}