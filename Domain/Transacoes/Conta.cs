using Domain.Base;
using System;

namespace Domain.Transacoes
{
    public sealed class Conta : BaseEntity
    {   
        public Banco Banco { get; private set; }
        public long Numero { get; private set; }
        public long Agencia { get; private set; }

        public Conta(Banco banco, long numero, long agencia)
        {
            Banco = banco;
            Numero = numero;
            Agencia = agencia;
        }

        public Conta(Guid id, Banco banco, long numero, long agencia)
        {
            AlterarIdentificador(id);
            Banco = banco;
            Numero = numero;
            Agencia = agencia;
        }

        public void AlterarBanco(Banco banco)
        {
            Banco = banco;
        }
    }
}