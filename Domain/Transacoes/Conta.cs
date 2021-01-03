using Domain.Base;
using System;

namespace Domain.Transacoes
{
    public sealed class Conta : BaseEntity
    {   
        public Banco Banco { get; private set; }
        public long Numero { get; private set; }
        public int Agencia { get; private set; }

        public Conta(Banco banco, long numero, int agencia)
        {
            Banco = banco;
            Numero = numero;
            Agencia = agencia;
        }

        public Conta(Guid id, Banco banco, long numero, int agencia)
        {
            AlterarIdentificador(id);
            Banco = banco;
            Numero = numero;
            Agencia = agencia;
        }
    }
}