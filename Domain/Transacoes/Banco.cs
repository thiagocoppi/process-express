using Domain.Base;
using System;

namespace Domain.Transacoes
{
    public sealed class Banco : BaseEntity
    {
        public string Nome { get; private set; }
        public int Codigo { get; private set; }

        public Banco(string nome, int codigo)
        {
            Nome = nome;
            Codigo = codigo;
        }

        public Banco(Guid id, string nome, int codigo)
        {
            Nome = nome;
            Codigo = codigo;
            AlterarIdentificador(id);
        }
    }
}